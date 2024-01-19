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
    public class RolesRepo : RepositoryBase<Membership_Roles>, IRolesRepo
    {
        public RolesRepo(PersistanceDBContext context, ICurrentUserSession currentUserSession) : base(context, currentUserSession)
        {

        }
        public async Task Insert(CreateRoleCommand request)
        {
            Membership_Roles roles = new Membership_Roles
            {
                IsAdmin = request.IsAdmin,
                Name = request.Name
            };

            request.PermissionIds.ForEach(p =>
            {
                roles.RolesPermission.Add(new Membership_RolesPermission
                {
                    PermissionId = p,
                });
            });
            await base.Save();

        }

        public async Task Update(UpdateRoleCommand request)
        {
            Membership_Roles roles = await GetAllAsQueryable(new string[] { nameof(Membership_RolesPermission) })
                .Where(p => p.Id == request.Id)
                .FirstAsync();
            roles.Name = request.Name;
            roles.IsAdmin = request.IsAdmin;
            updateRolePermissions(request, roles);
            await base.Save();
        }

        private static void updateRolePermissions(UpdateRoleCommand request, Membership_Roles roles)
        {
            IEnumerable<Membership_RolesPermission> oldPermissionIds = roles.RolesPermission;
            IEnumerable<int> deletedPermissions = oldPermissionIds.Where(p => !request.PermissionIds.Contains(p.PermissionId))
                .Select(p => p.PermissionId).ToList();
            List<int> newPermissions = request.PermissionIds.Except(deletedPermissions).ToList();

            roles.RolesPermission
                .Where(p => deletedPermissions.Contains(p.PermissionId))
                .ToList()
                .ForEach(p => roles.RolesPermission.Remove(p));

            newPermissions.ForEach(p =>
            {
                roles.RolesPermission.Add(new Membership_RolesPermission
                {
                    PermissionId = p,
                    RoleId = roles.Id
                });
            });
        }
    }
}
