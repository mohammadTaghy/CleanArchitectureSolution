using Application;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Extensions.Configuration;
using Domain.Entities;

namespace Persistence
{
    public class PersistanceDBContext : DbContext, IPersistanceDBContext
    {
        readonly IConfiguration _Configuration;
        public PersistanceDBContext(DbContextOptions<PersistanceDBContext> options):base(options)
        {

        }
        public PersistanceDBContext(DbContextOptions<PersistanceDBContext> options, IConfiguration configuration) : base(options)
        {
            _Configuration = configuration;
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
            //modelBuilder.HasDefaultSchema(_Configuration.GetSection("DbSetting").GetSection("Schema").Value);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersistanceDBContext).Assembly);
        }
    }
    //public static class ModelBuilderExtensions
    //{
    //    public static IEnumerable<IMutableEntityType> EntityTypes(this ModelBuilder builder)
    //    {
    //        return builder.Model.GetEntityTypes();
    //    }
    //    public static IEnumerable<IMutableProperty> Properties(this ModelBuilder builder)
    //    {
    //        return builder.EntityTypes().SelectMany(entityType => entityType.GetProperties());
    //    }

    //    public static IEnumerable<IMutableProperty> Properties<T>(this ModelBuilder builder)
    //    {
    //        return builder.EntityTypes().SelectMany(entityType => entityType.GetProperties().Where(x => x.ClrType == typeof(T)));
    //    }

    //    public static void Configure(this IEnumerable<IMutableEntityType> entityTypes, Action<IMutableEntityType> convention)
    //    {
    //        foreach (var entityType in entityTypes)
    //        {
    //            convention(entityType);
    //        }
    //    }

    //    public static void Configure(this IEnumerable<IMutableProperty> propertyTypes, Action<IMutableProperty> convention)
    //    {
    //        foreach (var propertyType in propertyTypes)
    //        {
    //            convention(propertyType);
    //        }
    //    }
    //}
}
