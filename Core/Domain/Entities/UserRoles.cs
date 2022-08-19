﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserRoles:Entity
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Roles Role { get; set; }
    }
}
