using Application.Common.Exceptions;
using Application.Common.Model;
using AutoMapper;
using Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Membership.Permission.Command.Update
{
    public class UpdatePermissionCommandHandler : BaseCommandHandler<UpdatePermissionCommand, CommandResponse<Membership_Permission>, IPermissionRepo>
    {
        public UpdatePermissionCommandHandler(IPermissionRepo repo, IMapper mapper, ICacheManager cacheManager) : base(repo, mapper, cacheManager)
        {
        }

        public override async Task<CommandResponse<Membership_Permission>> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new BadRequestException(string.Format(CommonMessage.NullException, "Permission"));

            Membership_Permission permission = _mapper.Map<Membership_Permission>(request);

            await _repo.Update(permission);
            return new CommandResponse<Membership_Permission>(true, permission);
        }
    }
}
