using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    public class UserEducationConfiguration : BaseConfiguration<Common_UserEducation>
    {
        public override void BaseConfigure(EntityTypeBuilder<Common_UserEducation> builder)
        {
            builder.HasOne(p => p.LessonsCategories)
                .WithMany(p => p.UserEducations).HasForeignKey(p => p.LessonsCategoriesId);
            builder.HasOne(p => p.User).WithMany(p => p.UserEducations).HasForeignKey(p => p.UserId);

                
        }
    }
}
