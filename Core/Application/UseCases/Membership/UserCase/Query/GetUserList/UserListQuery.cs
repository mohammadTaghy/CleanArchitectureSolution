using Application.Common.Model;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Membership.UserCase.Query.GetUserList
{
    public class UserListQuery : BaseLoadListQuery<QueryResponse<List<UserDto>>, Membership_User>
    {

    }
}
