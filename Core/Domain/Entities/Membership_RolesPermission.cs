using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Membership_RolesPermission : Entity
    {
        [Column(IsRequired = true, Title = "شناسه نقش")]
        public int RoleId { get ; set ; }
        [Column(IsRequired = true, Title = "شناسه مجوز")]
        public int PermissionId { get ; set ; }

        public Membership_Roles Role { get ; set ; }
        public Membership_Permission Permission { get ; set ; }
    }
}
