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
    public class RolesPermissionConfiguration : BaseConfiguration<Membership_RolesPermission>
    {
        public override void BaseConfigure(EntityTypeBuilder<Membership_RolesPermission> builder)
        {
            builder.HasOne(p => p.Role)
                .WithMany(p => p.RolesPermission)
                .HasForeignKey(p => p.RoleId);
            builder.HasOne(p => p.Permission)
                .WithMany(p => p.RolesPermissions)
                .HasForeignKey(p => p.PermissionId);


        }
    }
}
