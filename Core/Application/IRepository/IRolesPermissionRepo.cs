using Application.UseCases;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IRolesPermissionRepo : IRepositoryBase<Membership_RolesPermission>
    {
        Task<List<int>> GetPermissions(int? roleId);
        List<int> GetRolePermissions(List<int> rolesId);
        Task<bool> Insert(CreateRolesPermissionCommand request);
    }
}
