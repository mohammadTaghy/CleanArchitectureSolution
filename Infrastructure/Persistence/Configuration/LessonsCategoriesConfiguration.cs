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
            builder.Property(p=>p.)
        }
    }
}
