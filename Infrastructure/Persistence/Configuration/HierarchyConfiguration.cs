using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    public abstract class HierarchyConfiguration<T>:BaseConfiguration<T>
        where T : class, IHierarchyEntity<T>
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasOne(p => p.ParentEntity).WithMany(p => p.ChildList).HasForeignKey(p => p.ParentId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.Property(p => p.LevelChar).HasMaxLength(1);
            builder.Property(p => p.FullKeyCode).HasMaxLength(128);
            base.Configure(builder);
        }
    }
}
