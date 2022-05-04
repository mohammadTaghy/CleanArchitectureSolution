using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<IUser>
    {
        public void Configure(EntityTypeBuilder<IUser> builder)
        {
            builder.ToTable(nameof(User));
            builder.HasIndex(x => x.UserName).IsUnique()
                //.IncludeProperties<IUser>(p=>
                //new {
                //    p.FirstName,
                //    p.LastName})
                ;
            builder.Property(p => p.UserName).HasMaxLength(512).IsRequired();
            builder.Property(p => p.UserName).HasMaxLength(512).IsRequired();

        }
    }
}
