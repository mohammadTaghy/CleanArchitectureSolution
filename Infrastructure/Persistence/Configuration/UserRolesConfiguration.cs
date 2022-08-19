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
    internal class UserRolesConfiguration : IEntityTypeConfiguration<UserRoles>
    {
        public void Configure(EntityTypeBuilder<UserRoles> builder)
        {
            builder.ToTable(nameof(UserRoles));
            builder.HasKey(p => p.Id);
            builder.HasOne<Roles>(p=>p.Role)
                .WithMany(p=>p.UserRoles)
                .HasForeignKey(p=>p.RoleId);
            builder.HasOne<User>(p => p.User)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(p => p.UserId);



        }
    }
}
