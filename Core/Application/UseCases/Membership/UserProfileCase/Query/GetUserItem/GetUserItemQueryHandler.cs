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

namespace Application.UseCases.UserProfileCase.Query.GetUserItem
{
    public class GetUserItemQueryHandler : 
        BaseLoadItemQueryHandler<UserItemQuery, IUserProfileRepo, Membership_UserProfile, UserItemDto>
    {
        public GetUserItemQueryHandler(IUserProfileRepo userRepoRead, IMapper mappingProfile):base(userRepoRead,mappingProfile)
        {
          
        }
    }
}
