using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RolesPermission : Entity
    {
        [Column(IsRequired = true, Title = "شناسه نقش")]
        public int RolesId { get ; set ; }
        [Column(IsRequired = true, Title = "شناسه مجوز")]
        public int PermissionId { get ; set ; }

        public Roles Role { get ; set ; }
        public Permission Permission { get ; set ; }
    }
}
