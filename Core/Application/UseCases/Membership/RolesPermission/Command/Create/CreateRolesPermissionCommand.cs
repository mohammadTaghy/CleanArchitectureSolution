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
    public class CreateRolesPermissionCommand : IRequest<CommandResponse<bool>>
    {
        public int RolesId { get; set; }
        public List<int> PermissionIds { get; set; }
    }
}
