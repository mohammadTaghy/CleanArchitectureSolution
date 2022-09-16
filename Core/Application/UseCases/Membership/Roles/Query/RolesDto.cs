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
    public class RolesDto: IMapFrom<Roles>
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool IsAdmin { get; set; }
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Roles, RolesDto>();
        }
    }
}
