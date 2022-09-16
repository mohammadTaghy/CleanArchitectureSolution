using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Roles:Entity
    {
        [Column(IsRequired = true, Title = "نام نقش")]
        public string RoleName { get; set; }
        [Column(IsRequired = true, Title = "دارای دسترسی مدیریتی")]
        public bool IsAdmin { get; set; }
        public ICollection<RolesPermission> RolesPermission { get; set; }
        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
