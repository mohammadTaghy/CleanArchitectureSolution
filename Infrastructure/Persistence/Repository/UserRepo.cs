using Amazon.Auth.AccessControlPolicy;
using Application;
using Application.UseCases;
using Application.UseCases.UserCase.Command.Create;
using Application.UseCases.UserCase.Command.Update;
using Common;
using Domain;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class UserRepo : RepositoryBase<Membership_User>, IUserRepo
    {
        public UserRepo(PersistanceDBContext context, ICurrentUserSession currentUserSession) : base(context, currentUserSession)
        {

        }
        #region query method

        public async Task<bool> AnyEntity(Membership_User user)
        {
            return await base.AnyEntity(p => p.UserName == user.UserName);
        }

        public async Task<Membership_User> FindAsync(int? id, string userName, CancellationToken cancellationToken)
        {
            return await base.FindAsync(p => p.Id == id || p.UserName == userName, cancellationToken);
        }
        public async Task<List<int>> GetRolesId(int userId)
        {
            
            return await GetAllAsQueryable().Where(p=>p.Id == userId).SelectMany(p=>p.UserRoles.Select(q=>q.RoleId)).ToListAsync();
        }
        #endregion
        #region Manipulate
        public async Task Insert(CreateUserCommand command)
        {
            Membership_User user = new Membership_User()
            {
                UserName = command.UserName,
                Email = command.Email,
                MobileNumber = command.MobileNumber,
                UserCode = UtilizeFunction.GenerateStringAndNumberRandomCode(6),
                PasswordHash = UtilizeFunction.CreateMd5(command.Password),
            };
            if (!command.FirstName.IsNullOrEmpty())
                user.UserProfile = createUserProfile(command);
            await base.Save();
        }

        private Membership_UserProfile createUserProfile(CreateUserCommand command)
        {
            return new Membership_UserProfile { 
                BirthDate= DateTimeHelper.ToDateTime(command.BirthDate),
                EducationGrade= command.EducationGrade,
                FirstName= command.FirstName,
                LastName= command.LastName,
                NationalCode= command.NationalCode,
                PicturePath= command.PicturePath,
                PostalCode= command.PostalCode,
                UserDescription= command.UserDescription
            };
        }

        public async Task Update(UpdateUserCommand command)
        {
            Membership_User user = await GetAllAsQueryable(new string[] { nameof(Membership_UserProfile), nameof(Membership_UserRoles) })
                .FirstAsync(p => p.Id == command.Id || p.UserName == command.UserName);
            user.UserName = command.UserName;
            user.MobileNumber = command.MobileNumber;
            user.Email = command.Email;
            updateUserProfile(command, user);
            updateRolePermissions(command, user);
            await base.Save();
        }

        private void updateUserProfile(UpdateUserCommand command, Membership_User user)
        {
            if (!command.FirstName.IsNullOrEmpty())
            {
                user.UserProfile.FirstName = command.FirstName;
                user.UserProfile.LastName = command.LastName;
                user.UserProfile.BirthDate = DateTimeHelper.ToDateTime(command.BirthDate);
                user.UserProfile.PicturePath = command.PicturePath;
                user.UserProfile.Gender = command.Gender;
                user.UserProfile.EducationGrade = command.EducationGrade;
                user.UserProfile.NationalCode = command.NationalCode;
                user.UserProfile.PostalCode = command.PostalCode;
                user.UserProfile.UserDescription = command.UserDescription;
            }
        }

        private void updateRolePermissions(UpdateUserCommand request, Membership_User user)
        {
            IEnumerable<Membership_UserRoles> oldRoleIds = user.UserRoles;
            IEnumerable<int> deletedRoleIds = oldRoleIds.Where(p => !request.RoleIds.Contains(p.RoleId))
                .Select(p => p.RoleId).ToList();
            List<int> newRoleIds = request.RoleIds.Except(deletedRoleIds).ToList();

            user.UserRoles
                .Where(p => deletedRoleIds.Contains(p.RoleId))
                .ToList()
                .ForEach(p => user.UserRoles.Remove(p));

            newRoleIds.ForEach(p =>
            {
                user.UserRoles.Add(new Membership_UserRoles
                {
                    UserId = user.Id,
                    RoleId = p
                });
            });
        }

       
        #endregion


    }
}
