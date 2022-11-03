using Application.Common.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UserProfileCase.Query.GetUserList
{
    public class UserListQuery: IRequest<QueryResponse<List<UserProfileListDto>>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string NationalCode { get; set; }
        public string UserName { get; set; }
        public int Index { get; set; }
        public int PageSize { get; set; }
    }
}
