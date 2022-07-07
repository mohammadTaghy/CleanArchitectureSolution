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
    public class RolesPermissionConfiguration : IEntityTypeConfiguration<RolesPermission>
    {
        public void Configure(EntityTypeBuilder<RolesPermission> builder)
        {
            builder.ToTable(nameof(RolesPermission));
            builder.HasKey(p => p.Id);
            builder.HasOne<Roles>(p => p.Role)
                .WithMany(p => p.RolesPermission)
                .HasForeignKey(p => p.RolesId);
            builder.HasOne<Permission>(p => p.Permission)
                .WithMany(p => p.RolesPermissions)
                .HasForeignKey(p => p.PermissionId);


        }
    }
}
