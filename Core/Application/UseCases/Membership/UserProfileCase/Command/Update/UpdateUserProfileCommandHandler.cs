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

namespace Application.UseCases
{
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, CommandResponse<UserProfileListDto>>
    {

        private IUserProfileRepo _userProfileRepo;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mappingProfile;

        public UpdateUserProfileCommandHandler(IUserProfileRepo userProfileRepo, IUserRepo userRepo, IMapper mappingProfile)
        {

            _userProfileRepo = userProfileRepo;
            _userRepo = userRepo;
            _mappingProfile = mappingProfile;
        }

        public async Task<CommandResponse<UserProfileListDto>> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException("", string.Format(CommonMessage.NullException, "UserProfile"));
            if (request.FirstName == null || request.LastName == null || request.MobileNumber == null)
                throw new ArgumentNullException("", string.Format(CommonMessage.NullException, $"{nameof(UpdateUserProfileCommand.FirstName)}-{nameof(UpdateUserProfileCommand.LastName)}-{nameof(UpdateUserProfileCommand.MobileNumber)}"));
            Membership_User user = await _userRepo.FindAsync(request.Id.Value);
            if (user == null)
            {
                user.MobileNumber = request.MobileNumber;
                user.IsMobileNumberConfirmed = request.IsMobileNumberConfirmed;
                user.IsEmailConfirmed = request.IsEmailConfirmed;
                user.IsUserConfirm = request.IsUserConfirm;
                user.IsEmailConfirmed = request.IsEmailConfirmed;
                user.ManagerConfirm = (byte)Constants.Status.InCheck;
                user.Email = request.Email;
                if (!string.IsNullOrEmpty(request.Password))
                    user.PasswordHash = UtilizeFunction.CreateMd5(request.Password);
                user.UserName = request.UserName;
                Membership_UserProfile userProfile = user.UserProfile;
                userProfile.BirthDate=DateTimeHelper.ToDateTime(request.BirthDate);
                userProfile.PicturePath= request.PicturePath;
                userProfile.Gender= request.Gender;
                userProfile.EducationGrade= request.EducationGrade;
                userProfile.FirstName= request.FirstName;
                userProfile.LastName= request.LastName;
                userProfile.NationalCode= request.NationalCode;
                userProfile.PostalCode= request.PostalCode;
                userProfile.UserDescription= request.UserDescription;
                await _userRepo.Save();
            }
            return await Task.FromResult(new CommandResponse<UserProfileListDto>(true, _mappingProfile.Map<UserProfileListDto>(request)));
        }
    }
}
