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
    public class DeleteUserProfileCommandHandler : IRequestHandler<DeleteUserProfileCommand, CommandResponse<bool>>
    {

        private IUserProfileRepo _userProfileRepo;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mappingProfile;

        public DeleteUserProfileCommandHandler(IUserProfileRepo userProfileRepo, IUserRepo userRepo, IMapper mappingProfile)
        {

            _userProfileRepo = userProfileRepo;
            _userRepo = userRepo;
            _mappingProfile = mappingProfile;
        }

        public async Task<CommandResponse<bool>> Handle(DeleteUserProfileCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException("", string.Format(CommonMessage.NullException, "UserProfile"));
            
            return await Task.FromResult(new CommandResponse<bool>(true, await _userRepo.DeleteItem(request.Id)));
        }
    }
}
