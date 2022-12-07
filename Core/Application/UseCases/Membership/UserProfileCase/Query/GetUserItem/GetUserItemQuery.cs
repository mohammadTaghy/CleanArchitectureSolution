using Application.Common.Model;
using Application.UseCases.UserProfileCase.Query.GetUserList;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UserProfileCase.Query.GetUserItem
{
    public class UserItemQuery : BaseLoadItemQuery<QueryResponse<UserItemDto>>
    {
    }
}
