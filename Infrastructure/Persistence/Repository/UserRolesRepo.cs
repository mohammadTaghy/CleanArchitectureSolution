using Application;
using Application.UseCases;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class UserRolesRepo : RepositoryBase<UserRoles>,IUserRolesRepo
    {
        public UserRolesRepo(PersistanceDBContext context) : base(context)
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
                List<int> oldRoleIds = await GetAllAsQueryable().Where(p => p.UserId == request.UserId).
                Select(p => p.RoleId).ToListAsync();
                List<int> deletedRoles = oldRoleIds.Except(request.RoleIds).ToList();
                List<int> newRoles = request.RoleIds.Except(deletedRoles).ToList();
                deletedRoles.ForEach(p => base.Context.Entry(p).State = EntityState.Deleted);
                newRoles.ForEach(p => new UserRoles
                {
                    RoleId = p,
                    UserId = request.UserId
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
