using Application.Common.Exceptions;
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
    public class PermissionsAsTreeQueryHandler : 
        BaseLoadListQueryHandler<PermissionsAsTreeQuery, IPermissionRepoRead, Membership_Permission, PermissionTreeDto>
    {
        public PermissionsAsTreeQueryHandler(IPermissionRepoRead repo, IMapper mapper, ICacheManager cacheManager) : base(repo, mapper,cacheManager)
        {
        }

        public async Task<QueryResponse<List<PermissionTreeDto>>> Handle(PermissionsAsTreeQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new BadRequestException(string.Format(CommonMessage.NullException, "Permission"));

            List<PermissionTreeDto> permissionTreeDtos = await _repo.GetPermissions(request.RoleId);

           

            return QueryResponse<List<PermissionTreeDto>>.CreateInstance(permissionTreeDtos, "", permissionTreeDtos.Count, true);
        }
    }
}
