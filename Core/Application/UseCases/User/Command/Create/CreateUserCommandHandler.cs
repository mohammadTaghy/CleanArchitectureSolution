using Application.Common;
using Application.Decorators;
using Application.Validation;
using AutoMapper;
using Common;
using Domain;
using MediatR;

namespace Application.UseCases.User.Command.Create
{

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CommandRequest<int>>
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

        public Task<CommandRequest<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            
            IUserRead user = _mappingProfile.Map<UserRead>(request);

            var validate = _userValidation.ValidateAsync(user, cancellationToken);
            if (validate.IsFaulted)
            {

                return Task.FromResult(
                    new CommandRequest<int>(validate.Result.Errors.Select(p=>new CustomError { ErrorMessage=p.ErrorMessage,ErrorCode=p.ErrorCode})));
            }
            var userInserted= _userRepo.Insert(user);
            
            ///Call event for insert into read DB
            return Task.FromResult(new CommandRequest<int>(true,user.Id));
        }

    }

}
