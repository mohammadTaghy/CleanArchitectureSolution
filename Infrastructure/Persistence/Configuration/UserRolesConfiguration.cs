using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    internal class UserRolesConfiguration : BaseConfiguration<Membership_UserRoles>
    {
        public override void BaseConfigure(EntityTypeBuilder<Membership_UserRoles> builder)
        {
            builder.HasOne<Membership_Roles>(p=>p.Role)
                .WithMany(p=>p.UserRoles)
                .HasForeignKey(p=>p.RoleId);
            builder.HasOne<Membership_User>(p => p.User)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(p => p.UserId);



        }
    }
}
