using Application.Common.Model;
using Application.IRepository;
using Application.Mappings;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Domain;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Membership.UserCase.Query.GetUserList
{
    public class UserListQueryHandler :
        BaseLoadListQueryHandler<UserListQuery, IUserRepoRead, Membership_User, UserDto>
    {
        public UserListQueryHandler(IUserRepoRead userRepoRead, IMapper mapping, ICacheManager cacheManager) : base(userRepoRead, mapping, cacheManager)
        {

        }

    }
}
