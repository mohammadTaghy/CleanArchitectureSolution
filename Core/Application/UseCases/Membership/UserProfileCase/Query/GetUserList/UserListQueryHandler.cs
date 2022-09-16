using Application.Mappings;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Domain;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UserProfileCase.Query.GetUserList
{
    public class UserListQueryHandler : IRequestHandler<UserListQuery, UserListResponse>
    {
        private readonly IUserProfileRepo _userProfileRepoRead;
        public readonly IMapper _mappingProfile;


        public UserListQueryHandler(IUserProfileRepo userRepoRead, IMapper mappingProfile)
        {
            _userProfileRepoRead = userRepoRead;
            _mappingProfile = mappingProfile;
        }


        public async Task<UserListResponse> Handle(UserListQuery request, CancellationToken cancellationToken)
        {
            if(request == null)
                throw new ArgumentNullException("", string.Format(CommonMessage.NullException, "Request"));
            if (request.Index < 0)
                request.Index = 0;
            int total = 0;
            List<UserProfile> userProfiles = await _userProfileRepoRead.ItemList(request,out total);
            return new UserListResponse
            {
                Total = total,
                UserList=_mappingProfile.Map<List<UserProfileListDto>>(userProfiles)
            };
            
        }
    }
}
