using Application.Common.Model;
using AutoMapper;
using Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class PermissionsAsTreeQueryHandler : BaseCommandHandler<PermissionsAsTreeQuery, QueryResponse<List<PermissionTreeDto>>, IPermissionRepo>
    {
        public PermissionsAsTreeQueryHandler(IPermissionRepo repo, IMapper mapper) : base(repo, mapper)
        {
        }

        public override async Task<QueryResponse<List<PermissionTreeDto>>> Handle(PermissionsAsTreeQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException("", string.Format(CommonMessage.NullException, "Permission"));
            List<PermissionTreeDto> permissionTreeDtos = await _repo.GetPermissions(request.RoleId);
            if (permissionTreeDtos == null || permissionTreeDtos.Count == 0)
                return QueryResponse<List<PermissionTreeDto>>.CreateInstance(new(), CommonMessage.EmptyResponse , 0, false);
            return QueryResponse<List<PermissionTreeDto>>.CreateInstance(permissionTreeDtos, "", permissionTreeDtos.Count, true);
        }
    }
}
