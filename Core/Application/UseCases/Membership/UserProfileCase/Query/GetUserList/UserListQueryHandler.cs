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
    public class UserListQueryHandler : 
        BaseLoadListQueryHandler<UserListQuery, IUserProfileRepo,Membership_UserProfile, UserProfileListDto>
    {
        public UserListQueryHandler(IUserProfileRepo userRepoRead, IMapper mappingProfile):base(userRepoRead,mappingProfile)
        {
           
        }

    }
}
