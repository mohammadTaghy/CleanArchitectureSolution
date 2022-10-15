using Application.Common.Exceptions;
using Application.Common.Model;
using Application.Validation;
using AutoMapper;
using Common;
using Domain;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.UserCase.Command.Create
{

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CommandResponse<int>>
    {
        private IUserValidation _userValidation;
        private IUserRepo _userRepo;
        private readonly IMapper _mappingProfile;

        public CreateUserCommandHandler(IUserValidation userValidation, IUserRepo userRepo, IMapper mappingProfile)
        {
            _userRepo = userRepo;
            _mappingProfile = mappingProfile;
            _userValidation = userValidation;
        }

        public async Task<CommandResponse<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            if (request == null)
                throw new ArgumentNullException("", string.Format(CommonMessage.NullException, "User"));

            Membership_User user = _mappingProfile.Map<Membership_User>(request);
            bool existUser = await _userRepo.AnyEntity(user);
            if (existUser)
                throw new ValidationException(new List<FluentValidation.Results.ValidationFailure>
                {
                    new FluentValidation.Results.ValidationFailure(nameof(Membership_User.UserName), String.Format(CommonMessage.IsDuplicateUserName, user.UserName))
                });
            user.UserCode = UtilizeFunction.GenerateStringAndNumberRandomCode(6);
            await _userRepo.Insert(user);

            ///Call event for insert into read DB
            return await Task.FromResult(new CommandResponse<int>(true, user.Id));
        }

    }

}
