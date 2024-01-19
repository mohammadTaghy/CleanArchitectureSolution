using Application.Common.Model;
using AutoMapper;
using Common;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class CurrentUserPermissionsAsTreeQueryHandler : 
        BaseLoadListQueryHandler<CurrentUserPermissionsAsTreeQuery, IPermissionRepoRead, Membership_Permission, PermissionTreeDto>
    {
       

        public CurrentUserPermissionsAsTreeQueryHandler(IPermissionRepoRead rolesPermissionRepo, IMapper mapper, ICacheManager cacheManager) :base(rolesPermissionRepo,mapper, cacheManager)
        {
           
        }

        public async Task<QueryResponse<List<PermissionTreeDto>>> Handle(CurrentUserPermissionsAsTreeQuery request, CancellationToken cancellationToken)
        {

            List<PermissionTreeDto> permissionTreeDtos = await _repo.GetCurrentRolePermissions(request.UserId);
            if (permissionTreeDtos == null || permissionTreeDtos.Count == 0)
                return QueryResponse<List<PermissionTreeDto>>.CreateInstance(new(), CommonMessage.Unauthorized, 0, false);

            return QueryResponse<List<PermissionTreeDto>>.CreateInstance(permissionTreeDtos, "", permissionTreeDtos.Count, true);
        }
    }
}
