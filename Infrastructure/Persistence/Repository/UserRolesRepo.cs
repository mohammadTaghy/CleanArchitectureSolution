using Application;
using Application.UseCases;
using Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class UserRolesRepo : RepositoryBase<Membership_UserRoles>,IUserRolesRepo
    {
        public UserRolesRepo(PersistanceDBContext context, ICurrentUserSession currentUserSession) : base(context,currentUserSession)
        {
        }
        #region CustomGet

        public Task<List<int>> GetRolesId(int userId)
        {
            return GetAllAsQueryable().Where(p => p.UserId == userId).Select(p => p.RoleId).ToListAsync();
        }
        #endregion
        #region Manipulate

        public async Task<bool> Insert(CreateUserRolesCommand request)
        {
            try
            {
                List<Membership_UserRoles> oldRoleIds = await GetAllAsQueryable().Where(p => p.UserId == request.UserId).ToListAsync();
                List<int> deletedRoles = oldRoleIds.Where(p=>!request.RoleIds.Contains(p.RoleId)).Select(p=>p.RoleId).ToList();
                List<int> newRoles = request.RoleIds.Except(deletedRoles).ToList();
                oldRoleIds.Where(p=>deletedRoles.Contains(p.RoleId)).ToList().ForEach(p => Delete(p));
                newRoles.ForEach(p => {
                    var entity= new Membership_UserRoles
                    {
                        RoleId = p,
                        UserId = request.UserId
                    };
                    Add(entity);
                });
                await base.Save();
                return true;

            }
            catch (Exception)
            {
                //log
                return false;
            }

        }
        #endregion

    }
}
