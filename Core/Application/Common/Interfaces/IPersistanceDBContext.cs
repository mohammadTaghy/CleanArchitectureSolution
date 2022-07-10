using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IPersistanceDBContext
    {
        DbSet<User> Users { get; set; }
        DbSet<UserProfile> Profiles { get; set; }
        DbSet<Permission> Permissions { get; set; }
        DbSet<Roles> Roles { get; set; }
        DbSet<RolesPermission> RolesPermissions { get; set; }
        DbSet<UserRoles> UserRoles { get; set; }
    }
}
