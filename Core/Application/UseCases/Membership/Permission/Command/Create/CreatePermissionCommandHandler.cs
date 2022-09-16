using Application.Common.Model;
using Application.Mappings;
using Application.UseCases.UserProfileCase.Command.Create;
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
    public class CreatePermissionCommandHandler : BaseCommandHandler<CreatePermissionCommand, CommandResponse<Permission>, IPermissionRepo>
    {
        public CreatePermissionCommandHandler(IPermissionRepo permissionRepo, IMapper mapper) 
            : base(permissionRepo, mapper)
        {

        }

        public override async Task<CommandResponse<Permission>> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException("", string.Format(CommonMessage.NullException, "Permission"));
            Permission permission = _mapper.Map<Permission>(request);
            await _repo.Insert(permission);
            return new CommandResponse<Permission>(true, permission);

        }

       
    }
}
