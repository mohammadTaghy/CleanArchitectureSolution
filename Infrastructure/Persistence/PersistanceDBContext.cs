using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Extensions.Configuration;
using Domain.Entities;
using Application.Common.Interfaces;

namespace Persistence
{
    public class PersistanceDBContext : DbContext, IPersistanceDBContext
    {
        public PersistanceDBContext(DbContextOptions<PersistanceDBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> Profiles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<RolesPermission> RolesPermissions { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        DbSet<User> IPersistanceDBContext.Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersistanceDBContext).Assembly);
        }
    }

}
