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
    internal class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable(nameof(UserProfile));
            

            builder.Property(p=>p.FirstName).HasMaxLength(1024).IsRequired();
            builder.Property(p => p.LastName).HasMaxLength(1024).IsRequired();
            builder.Property(p => p.EducationGrade).HasMaxLength(512);
            builder.Property(p => p.NationalCode).HasMaxLength(10);
            builder.Property(p => p.PicturePath).HasMaxLength(2048);
            builder.Property(p => p.PostalCode).HasMaxLength(11);
            builder.Property(p => p.UserDescription).HasMaxLength(2048);
        }
    }
}
