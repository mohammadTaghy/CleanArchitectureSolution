using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Membership.UserCase.Query.GetUserList
{
    public class UserListResponse
    {
        public List<UserDto> UserList { get; set; }
        public int Total { get; set; }
    }
}
