using Application.Common.Model;
using AutoMapper;
using Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class CreateUserRolesCommandHandler : BaseCommandHandler<CreateUserRolesCommand, CommandResponse<bool>, IUserRolesRepo>
    {
        public CreateUserRolesCommandHandler(IUserRolesRepo repo, IMapper mapper) : base(repo, mapper)
        {
        }

        public override async Task<CommandResponse<bool>> Handle(CreateUserRolesCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException("", string.Format(CommonMessage.NullException, "UserRoles"));
            bool saveIsComplete = await _repo.Insert(request);
            if (!saveIsComplete)
                throw new Exception(CommonMessage.Error);
            return new CommandResponse<bool>(saveIsComplete);
        }
    }
}
