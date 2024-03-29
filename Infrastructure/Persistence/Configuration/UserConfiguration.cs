﻿using Domain;
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
    public class UserConfiguration : BaseConfiguration<Membership_User>
    {
        public override void BaseConfigure(EntityTypeBuilder<Membership_User> builder)
        {
            builder.HasIndex(x => x.UserName).IsUnique()
                //.IncludeProperties<IUser>(p=>
                //new {
                //    p.FirstName,
                //    p.LastName})
                ;
            builder.Property(p => p.UserName).HasMaxLength(512).IsRequired();
            builder.Property(p => p.UserCode).HasMaxLength(20).IsRequired();
            builder.Property(p => p.DeviceId).HasMaxLength(512);
            builder.Property(p => p.Email).HasMaxLength(128).IsRequired();
            builder.Property(p => p.MobileNumber).HasMaxLength(12).IsRequired();
            builder.Property(p => p.PasswordHash).HasMaxLength(2048).IsRequired();

        }
    }
}
