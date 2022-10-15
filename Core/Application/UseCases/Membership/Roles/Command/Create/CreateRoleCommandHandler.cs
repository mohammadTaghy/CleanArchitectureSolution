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
    public class CreateRoleCommandHandler : BaseCommandHandler<CreateRoleCommand, CommandResponse<Membership_Roles>, IRolesRepo>
    {
        public CreateRoleCommandHandler(IRolesRepo repo, IMapper mapper) : base(repo, mapper)
        {
        }

        public override async Task<CommandResponse<Membership_Roles>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            if(request is null)
                throw new ArgumentNullException("", string.Format(CommonMessage.NullException, "Roles"));
            Membership_Roles role = _mapper.Map<Membership_Roles>(request);
            if (await _repo.AnyEntity(p => p.RoleName == role.RoleName))
                throw new ValidationException(new List<FluentValidation.Results.ValidationFailure>
                {
                    new FluentValidation.Results.ValidationFailure(nameof(Membership_Roles.RoleName),String.Format(CommonMessage.IsDuplicate,"نام نقش"))
                });
            await _repo.Insert(role);
            return new CommandResponse<Membership_Roles>(true,role);

        }
    }
}
