using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Persistence
{
    public class PersistanceDBContext : DbContext//, IPersistanceDBContext
    {
        public PersistanceDBContext(DbContextOptions<PersistanceDBContext> options) : base(options)
        {

        }

        public DbSet<Membership_User> Users { get; set; }
        public DbSet<Membership_UserProfile> Profiles { get; set; }
        public DbSet<Membership_Permission> Permissions { get; set; }
        public DbSet<Membership_Roles> Roles { get; set; }
        public DbSet<Membership_RolesPermission> RolesPermissions { get; set; }
        public DbSet<Membership_UserRoles> UserRoles { get; set; }
        public DbSet<Common_LessonsCategories> LessonsCategories { get; set; }
        public DbSet<Common_UserEducation> UserEducations { get; set; }

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
