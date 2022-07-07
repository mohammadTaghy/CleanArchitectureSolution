using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Users.Query.GetUserList
{
    public class UserListQueryRequest: IRequest<List<UserListDto>>
    {
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string NationalCode { get; set; }
        public string UserName { get; set; }
        public int Index { get; set; }
        public int PageSize { get; set; }
    }
}
