using AutoMapper;
using Common;
using Domain.Entities;

namespace Application.UseCases.Membership.UserCase.Query.GetUserItem
{
    public class GetUserItemQueryHandler :
        BaseLoadItemQueryHandler<UserItemQuery, IUserRepoRead, Membership_User, UserDto>
    {
        public GetUserItemQueryHandler(IUserRepoRead userRepoRead, IMapper mappingProfile, ICacheManager cacheManager) : base(userRepoRead, mappingProfile, cacheManager)
        {

        }
    }
}
