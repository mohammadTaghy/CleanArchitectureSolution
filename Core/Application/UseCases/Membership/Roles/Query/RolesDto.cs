using Application.Mappings;
using Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public record RolesDto(int Id, string Name, bool IsAdmin, List<int> permissionIds) : IMapFrom<Membership_Roles>
    {
        public RolesDto():this(0,"",false,new List<int>())
        {

        }
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Membership_Roles, RolesDto>()
                .ForMember(d=>d.permissionIds,source=>source.MapFrom(p=>p.RolesPermission.Select(q=>q.PermissionId)));
        }
    }
}
