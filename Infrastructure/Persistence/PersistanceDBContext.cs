using Application;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Extensions.Configuration;
namespace Persistence
{
    public class PersistanceDBContext : DbContext, IPersistanceDBContext
    {
        IConfiguration _Configuration;
        public PersistanceDBContext(DbContextOptions<PersistanceDBContext> options, IConfiguration configuration) : base(options)
        {
            _Configuration = configuration;
        }

        public DbSet<IUser> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(_Configuration.GetSection("DbSetting").GetSection("Schema").Value);
            base.OnModelCreating(modelBuilder);
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
