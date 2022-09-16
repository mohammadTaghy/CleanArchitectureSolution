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

namespace Application.UseCases.UserProfileCase.Command.Create
{
    public class CreateUserProfileCommandHandler : IRequestHandler<CreateUserProfileCommand, CommandResponse<int>>
    {

        private IUserProfileRepo _userProfileRepo;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mappingProfile;

        public CreateUserProfileCommandHandler(IUserProfileRepo userProfileRepo, IUserRepo userRepo, IMapper mappingProfile)
        {

            _userProfileRepo = userProfileRepo;
            _userRepo = userRepo;
            _mappingProfile = mappingProfile;
        }

        public async Task<CommandResponse<int>> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException("", string.Format(CommonMessage.NullException, "UserProfile"));
            if (request.FirstName == null || request.LastName == null || request.MobileNumber == null)
                throw new ArgumentNullException("", string.Format(CommonMessage.NullException, $"{nameof(CreateUserProfileCommand.FirstName)}-{nameof(CreateUserProfileCommand.LastName)}-{nameof(CreateUserProfileCommand.MobileNumber)}"));
            UserProfile userProfile = _mappingProfile.Map<UserProfile>(request);
            User user = await _userRepo.FindAsync(request.UserId, request.UserName, cancellationToken);
            int id = 0;
            if (user == null)
            {
                user = new User
                {
                    Email = request.Email,
                    UserProfile = userProfile,
                    IsEmailConfirmed = false,
                    IsMobileNumberConfirmed = false,
                    IsUserConfirm = false,
                    ManagerConfirm = (byte)Constants.Status.InCheck,
                    MobileNumber = request.MobileNumber,
                    PasswordHash = UtilizeFunction.CreateMd5(request.Password),
                    UserCode = UtilizeFunction.GenerateStringAndNumberRandomCode(6),
                    UserName = request.UserName,
                };
                await _userRepo.Insert(user);
                id = user.Id;
            }
            else
            {
                userProfile.Id = user.Id;
                await _userProfileRepo.Insert(userProfile);
                id = userProfile.Id;

            }
            return await Task.FromResult(new CommandResponse<int>(true, id));
        }
    }
}
