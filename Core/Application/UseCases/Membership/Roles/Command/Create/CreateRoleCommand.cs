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
    public class CreateRoleCommand : IRequest<CommandResponse<Roles>>, IMapFrom<Roles>
    {
        public string RoleName { get; set; }
        public bool IsAdmin { get; set; }
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<CreateRoleCommand, Roles>();
        }
    }
}
