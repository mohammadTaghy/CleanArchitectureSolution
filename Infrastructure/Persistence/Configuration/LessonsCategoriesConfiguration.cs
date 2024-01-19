using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    public class LessonsCategoriesConfiguration : HierarchyConfiguration<Common_LessonsCategories>
    {
        public override void BaseConfigure(EntityTypeBuilder<Common_LessonsCategories> builder)
        {
            builder.HasMany(p => p.ChildList)
                .WithOne(p => p.ParentEntity)
                .HasForeignKey(p => p.ParentId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);

            builder.Property(p => p.Name).HasMaxLength(512).IsRequired();
            builder.Property(p => p.Title).HasMaxLength(512).IsRequired();
            builder.Property(p => p.IConPath).HasMaxLength(2048);
        }
    }
}
