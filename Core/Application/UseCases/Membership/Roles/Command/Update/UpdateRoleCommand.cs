using Application.Common.Model;
using Application.Mappings;
using Domain.Entities;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class UpdateRoleCommand : RoleCommandGlobalProperties, IRequest<CommandResponse<Membership_Roles>>, IMapFrom<Membership_Roles>
    {
        
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<UpdateRoleCommand, Membership_Roles>();
        }
    }
}
