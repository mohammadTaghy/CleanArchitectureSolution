using Application.Common.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UserProfileCase.Query.GetUserList
{
    public class UserListQuery : BaseLoadListQuery<QueryResponse<List<UserProfileListDto>>>
    {
       
    }
}
