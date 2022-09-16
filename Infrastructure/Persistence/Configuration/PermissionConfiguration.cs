﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable(nameof(Permission));
            builder.HasKey(p => p.Id);

            builder.HasMany(p => p.ChildList)
                .WithOne(p => p.ParentEntity)
                .HasForeignKey(p => p.ParentId);

            builder.Property(p => p.LevelChar).HasMaxLength(1);
            builder.Property(p => p.FullKeyCode).HasMaxLength(128);
            builder.Property(p => p.Name).HasMaxLength(512).IsRequired();
            builder.Property(p => p.Title).HasMaxLength(512).IsRequired();
            builder.Property(p => p.CommandName).HasMaxLength(1024).IsRequired();

        }
    }
}
