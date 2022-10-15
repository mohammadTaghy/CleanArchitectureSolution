using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Membership_Roles:Entity
    {
        [Column(IsRequired = true, Title = "نام نقش")]
        public string RoleName { get; set; }
        [Column(IsRequired = true, Title = "دارای دسترسی مدیریتی")]
        public bool IsAdmin { get; set; }
        public ICollection<Membership_RolesPermission> RolesPermission { get; set; }
        public ICollection<Membership_UserRoles> UserRoles { get; set; }
    }
}
