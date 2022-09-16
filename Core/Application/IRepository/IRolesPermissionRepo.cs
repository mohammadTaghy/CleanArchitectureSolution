using Application.UseCases;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IRolesPermissionRepo : IRepositoryBase<RolesPermission>
    {
        Task<List<PermissionTreeDto>> GetCurrentRolePermissions(int userId);
        Task<List<PermissionTreeDto>> GetPermissions(int roleId);
        Task<bool> Insert(CreateRolesPermissionCommand request);
    }
}
