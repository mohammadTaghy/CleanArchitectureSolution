using Application.Common.Model;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Membership.Permission.Command.Delete
{
    public class DeletePermissionCommand : IRequest<CommandResponse<bool>>
    {
        public int Id { get; set; }
    }
}
