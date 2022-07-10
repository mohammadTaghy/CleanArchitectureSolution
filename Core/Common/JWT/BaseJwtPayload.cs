using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.JWT
{
    public class BaseJwtPayload
    {
        public int CurrentVersionCode { get; set; }
        public int UserId { get; set; }
        public bool IsUsrConfirm { get; set; }
        public bool IsManagerConfirm { get; set; }
        public bool IsSecondRegister { get; set; }
    }
}
