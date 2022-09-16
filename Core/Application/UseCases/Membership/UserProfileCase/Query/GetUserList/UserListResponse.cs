using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UserProfileCase.Query.GetUserList
{
    public class UserListResponse
    {
        public List<UserProfileListDto> UserList { get; set; }
        public int Total { get; set; }
    }
}
