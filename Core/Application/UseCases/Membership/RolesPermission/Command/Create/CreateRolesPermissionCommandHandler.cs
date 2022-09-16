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
    public class CreateRolesPermissionCommandHandler : BaseCommandHandler<CreateRolesPermissionCommand, CommandResponse<bool>, IRolesPermissionRepo>
    {
        public CreateRolesPermissionCommandHandler(IRolesPermissionRepo repo, IMapper mapper) : base(repo, mapper)
        {
        }

        public override async Task<CommandResponse<bool>> Handle(CreateRolesPermissionCommand request, CancellationToken cancellationToken)
        {
            if(request is null)
                throw new ArgumentNullException("", string.Format(CommonMessage.NullException, "RolesPermission"));
            bool saveIsComplete= await _repo.Insert(request);
            if (!saveIsComplete)
                throw new Exception(CommonMessage.Error);
            return new CommandResponse<bool>(saveIsComplete);

        }
    }
}
