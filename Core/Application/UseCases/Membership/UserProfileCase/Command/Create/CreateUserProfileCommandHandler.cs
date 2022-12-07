using Application.Common.Model;
using Application.UseCases.UserProfileCase.Query.GetUserList;
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
    public class CreateUserProfileCommandHandler : IRequestHandler<CreateUserProfileCommand, CommandResponse<UserProfileListDto>>
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

        public async Task<CommandResponse<UserProfileListDto>> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException("", string.Format(CommonMessage.NullException, "UserProfile"));
            if (request.FirstName == null || request.LastName == null || request.MobileNumber == null)
                throw new ArgumentNullException("", string.Format(CommonMessage.NullException, $"{nameof(CreateUserProfileCommand.FirstName)}-{nameof(CreateUserProfileCommand.LastName)}-{nameof(CreateUserProfileCommand.MobileNumber)}"));
            Membership_UserProfile userProfile = _mappingProfile.Map<Membership_UserProfile>(request);
            Membership_User user = await _userRepo.FindAsync(request.Id, request.UserName, cancellationToken);
            if (user == null)
            {
                user = new Membership_User
                {
                    Email = request.Email,
                    IsEmailConfirmed = request.IsEmailConfirmed,
                    IsMobileNumberConfirmed = request.IsMobileNumberConfirmed,
                    IsUserConfirm = request.IsUserConfirm,
                    ManagerConfirm = (byte)Constants.Status.InCheck,
                    MobileNumber = request.MobileNumber,
                    PasswordHash = UtilizeFunction.CreateMd5(request.Password),
                    UserCode = UtilizeFunction.GenerateStringAndNumberRandomCode(6),
                    UserName = request.UserName,
                };
                user.UserProfile = userProfile;
                await _userRepo.Insert(user);
                userProfile.Id = user.Id;
                await _userProfileRepo.Insert(userProfile);
                //await Task.Factory.StartNew(() =>_userRepo.Insert(user),cancellationToken)
                //    .ContinueWith(t => { 
                //        userProfile.Id= user.Id; 
                //        _userProfileRepo.Insert(userProfile); 
                //    });
                request.Id=user.Id;

            }
            else
            {
                userProfile.Id = user.Id;
                await _userProfileRepo.Insert(userProfile);

            }
            return await Task.FromResult(new CommandResponse<UserProfileListDto>(true, _mappingProfile.Map<UserProfileListDto>(userProfile)));
        }
    }
}
