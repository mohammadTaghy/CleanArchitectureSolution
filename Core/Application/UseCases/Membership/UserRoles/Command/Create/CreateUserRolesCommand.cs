using Application.Common.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class CreateUserRolesCommand : IRequest<CommandResponse<bool>>
    {
        public int UserId { get; set; }
        public List<int> RoleIds { get; set; }
    }
}
