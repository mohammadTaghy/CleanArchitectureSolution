using Application.Common.Model;
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
    public class UserListQueryHandler : IRequestHandler<UserListQuery, QueryResponse<List<UserProfileListDto>>>
    {
        private readonly IUserProfileRepo _userProfileRepoRead;
        public readonly IMapper _mappingProfile;


        public UserListQueryHandler(IUserProfileRepo userRepoRead, IMapper mappingProfile)
        {
            _userProfileRepoRead = userRepoRead;
            _mappingProfile = mappingProfile;
        }


        public async Task<QueryResponse<List<UserProfileListDto>>> Handle(UserListQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException("", string.Format(CommonMessage.NullException, "Request"));
            if (request.Index < 0)
                request.Index = 0;
            int total = 0;

            List<Membership_UserProfile> userProfiles = await _userProfileRepoRead.ItemList(request, out total);
            return QueryResponse<List<UserProfileListDto>>.CreateInstance(
                _mappingProfile.Map<List<UserProfileListDto>>(userProfiles),
                String.Format(CommonMessage.SucceedData, "کاربر"),
                total,
                true);

        }
    }
}
