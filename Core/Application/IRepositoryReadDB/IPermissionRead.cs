using Application.UseCases;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IPermissionRepoRead : IRepositoryReadBase<Membership_Permission>
    {
        Task<List<PermissionTreeDto>> GetPermissions(int? roleId);
        Task<List<PermissionTreeDto>> GetCurrentRolePermissions(int userId);

    }
}
