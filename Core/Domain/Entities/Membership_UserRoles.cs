using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Membership_UserRoles:Entity, IEntity, ICreateProperty, IChangeProperty
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public Membership_User User { get; set; }
        public Membership_Roles Role { get; set; }
    }
}
