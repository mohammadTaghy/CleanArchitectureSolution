using AutoMapper;
using Common;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UserProfileCase.Query.GetUserItem
{
    public class GetUserItemQueryHandler : IRequestHandler<GetUserItemQuery, UserItemDto>
    {
        private readonly IUserProfileRepo _userProfileRepoRead;
        public readonly IMapper _mappingProfile;


        public GetUserItemQueryHandler(IUserProfileRepo userRepoRead, IMapper mappingProfile)
        {
            _userProfileRepoRead = userRepoRead;
            _mappingProfile = mappingProfile;
        }

        public async Task<UserItemDto> Handle(GetUserItemQuery request, CancellationToken cancellationToken)
        {
            if(request == null)
                throw new ArgumentNullException("", string.Format(CommonMessage.NullException, "Request"));
            UserProfile userProfile = await _userProfileRepoRead.FindAsync(request.Id, request.UserName,cancellationToken);
            return _mappingProfile.Map<UserItemDto>(userProfile);
        }
    }
}
