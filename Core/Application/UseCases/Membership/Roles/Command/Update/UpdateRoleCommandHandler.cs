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
    public class UpdateRoleCommandHandler : BaseCommandHandler<UpdateRoleCommand, CommandResponse<Membership_Roles>, IRolesRepo>
    {
        public UpdateRoleCommandHandler(IRolesRepo repo, IMapper mapper, ICacheManager cacheManager) : base(repo, mapper, cacheManager)
        {
        }

        public override async Task<CommandResponse<Membership_Roles>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException("", string.Format(CommonMessage.NullException, "Roles"));

            if (!await _repo.AnyEntity(p =>  p.Name == request.Name))
                throw new NotFoundException("Role", request.Id);

            Membership_Roles role = _mapper.Map<Membership_Roles>(request);
            
            await _repo.Update(request);
            return new CommandResponse<Membership_Roles>(true, role);

        }
    }
}
