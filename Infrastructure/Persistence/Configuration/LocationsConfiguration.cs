using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    public class LocationsConfiguration : HierarchyConfiguration<Membership_Locations>
    {
        public override void BaseConfigure(EntityTypeBuilder<Membership_Locations> builder)
        {
            
            builder.Property(p => p.Name).HasMaxLength(512).IsRequired();
            builder.Property(p => p.Title).HasMaxLength(512).IsRequired();
            builder.Property(p => p.IConPath).HasMaxLength(2048);
            builder.Property(p => p.NumberCode).HasMaxLength(5);
        }
    }
}
