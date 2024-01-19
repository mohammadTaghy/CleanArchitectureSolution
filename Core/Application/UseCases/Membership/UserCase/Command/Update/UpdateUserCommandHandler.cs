using Application.Common.Exceptions;
using Application.Common.Model;
using Application.Validation;
using AutoMapper;
using Common;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UserCase.Command.Update
{

    public class UpdateUserCommandHandler : BaseCommandHandler<UpdateUserCommand, CommandResponse<bool>, IUserRepo>
    {

        public UpdateUserCommandHandler(IUserRepo userRepo, IMapper mapper, ICacheManager cacheManager)
            : base(userRepo, mapper, cacheManager)
        {
        }
        public override  async Task<CommandResponse<bool>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            _repo.CancellationToken = cancellationToken;

            if (command.UserName == null && command.Id == 0)
                throw new ArgumentNullException("", string.Format(CommonMessage.NullException, $"{nameof(UpdateUserCommand.Id)} یا {nameof(UpdateUserCommand.UserName)}"));

            Membership_User user = await _repo.FindAsync(command.Id, command.UserName, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException("کاربر", new { command.Id, command.UserName });
            }

            await _repo.Update(command);

            ///Call event for insert into read DB
            return await Task.FromResult(new CommandResponse<bool>(true));
        }
    }
}
