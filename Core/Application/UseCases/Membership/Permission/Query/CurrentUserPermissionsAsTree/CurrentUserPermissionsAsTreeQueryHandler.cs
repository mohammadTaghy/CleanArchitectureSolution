using Application.Common.Model;
using Application.UseCases.UserProfileCase.Query.GetUserItem;
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
        BaseLoadListQueryHandler<CurrentUserPermissionsAsTreeQuery, IPermissionRepo, Membership_Permission, PermissionTreeDto>
    {
       

        public CurrentUserPermissionsAsTreeQueryHandler(IPermissionRepo rolesPermissionRepo, IMapper mapper):base(rolesPermissionRepo,mapper)
        {
           
        }

        public override async Task<QueryResponse<List<PermissionTreeDto>>> Handle(CurrentUserPermissionsAsTreeQuery request, CancellationToken cancellationToken)
        {

            List<PermissionTreeDto> permissionTreeDtos = await _repo.GetCurrentRolePermissions(request.UserId);
            if (permissionTreeDtos == null || permissionTreeDtos.Count == 0)
                return QueryResponse<List<PermissionTreeDto>>.CreateInstance(new(), CommonMessage.Unauthorized, 0, false);
            return QueryResponse<List<PermissionTreeDto>>.CreateInstance(permissionTreeDtos, "", permissionTreeDtos.Count, true);
        }
    }
}
