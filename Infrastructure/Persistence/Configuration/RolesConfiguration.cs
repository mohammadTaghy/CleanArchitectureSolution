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
    public class RolesConfiguration : BaseConfiguration<Membership_Roles>
    {
        public override void BaseConfigure(EntityTypeBuilder<Membership_Roles> builder)
        {
            builder.Property(p => p.RoleName).HasMaxLength(512).IsRequired();
        }
    }
}
