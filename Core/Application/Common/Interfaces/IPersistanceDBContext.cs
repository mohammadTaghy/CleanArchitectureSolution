using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IPersistanceDBContext
    {
        DbSet<Membership_User> Users { get; set; }
        DbSet<Membership_UserProfile> Profiles { get; set; }
        DbSet<Membership_Permission> Permissions { get; set; }
        DbSet<Membership_Roles> Roles { get; set; }
        DbSet<Membership_RolesPermission> RolesPermissions { get; set; }
        DbSet<Membership_UserRoles> UserRoles { get; set; }
    }
}
