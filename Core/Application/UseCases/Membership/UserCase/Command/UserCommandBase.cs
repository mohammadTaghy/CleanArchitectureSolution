using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UserCase.Command
{
    public class UserCommandBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsMobileNumberConfirmed { get; set; }
        public bool IsUserConfirm { get; set; }
    }
}
