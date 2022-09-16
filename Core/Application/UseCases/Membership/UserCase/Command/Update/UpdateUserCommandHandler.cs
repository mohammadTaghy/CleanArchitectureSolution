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

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, CommandResponse<bool>>
    {
        private readonly IUserValidation userValidation;
        private readonly IUserRepo userRepo;
        private readonly IMapper _mappingProfile;

        public UpdateUserCommandHandler(IUserValidation userValidation, IUserRepo userRepo, IMapper mappingProfile)
        {
            this.userRepo = userRepo;
            this.userValidation = userValidation;
            _mappingProfile = mappingProfile;
        }
        public async Task<CommandResponse<bool>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            userRepo.CancellationToken = cancellationToken;
            if (command.UserName == null && command.Id == 0)
                throw new ArgumentNullException("",string.Format(CommonMessage.NullException, $"{nameof(UpdateUserCommand.Id)} یا {nameof(UpdateUserCommand.UserName)}"));
            User user = await userRepo.FindAsync(command.Id,command.UserName,cancellationToken);

            if (user == null)
            {
                throw new NotFoundException("کاربر", new { command.Id,command.UserName });
            }
            user = _mappingProfile.Map<User>(command);
            await userRepo.Update(user);

            ///Call event for insert into read DB
            return await Task.FromResult(new CommandResponse<bool>(true));
        }
    }
}
