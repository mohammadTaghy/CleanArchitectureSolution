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
    public class RolesPermissionRepo : RepositoryBase<Membership_RolesPermission>, IRolesPermissionRepo
    {
        public RolesPermissionRepo(PersistanceDBContext context, ICurrentUserSession currentUserSession) : base(context, currentUserSession)
        {
        }

        


        #region CustomGet
        public List<int> GetRolePermissions(List<int> rolesId)
        {
            return GetAllAsQueryable().Where(p=>rolesId.Contains(p.RoleId))
                .Select(p=>p.PermissionId)
                .ToList();
        }
        public async Task<List<int>> GetPermissions(int? roleId)
        {
            return await GetAllAsQueryable().Where(p => p.RoleId == roleId).Select(p => p.PermissionId).ToListAsync();
        }
        #endregion
        #region Manipulate
        public async Task<bool> Insert(CreateRolesPermissionCommand request)
        {
            try
            {
                List<Membership_RolesPermission> oldPermissionIds = await GetAllAsQueryable().Where(p => p.RoleId == request.RolesId).ToListAsync();
                List<int> deletedPermissions = oldPermissionIds.Where(p=>!request.PermissionIds.Contains(p.PermissionId))
                    .Select(p=>p.PermissionId).ToList();
                List<int> newPermissions = request.PermissionIds.Except(deletedPermissions).ToList();
                oldPermissionIds.Where(p=>deletedPermissions.Contains(p.PermissionId)).ToList().ForEach(p => Delete(p));
                newPermissions.ForEach(p => {
                    var entity = new Membership_RolesPermission
                    {
                        PermissionId = p,
                        RoleId = request.RolesId
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
        #region PrivateMethod
        
        #endregion
    }
}
