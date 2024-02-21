using Application;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Authentication
{
    public interface IIdentityStoreHelper
    {
        Task<IList<Claim>> GetClaimsAsync(int userId, CancellationToken cancellationToken);
    }

    public class IdentityStoreHelper : IIdentityStoreHelper
    {
        private readonly IUserRepoRead userRepoRead;

        public IdentityStoreHelper(IUserRepoRead userRepoRead)
        {
            this.userRepoRead = userRepoRead;
        }

        #region IUserClaimStore
        public async Task<IList<Claim>> GetClaimsAsync(int userId, CancellationToken cancellationToken)
        {
            Membership_User membership_User = await userRepoRead.FindOne(userId);
            List<string> permissionName = membership_User.UserRoles.SelectMany(p => p.Role.RolesPermission.Select(q => q.Permission.Name)).ToList();
            return new List<Claim>()
            {
                new Claim("Permissions",JsonSerializer.Serialize(permissionName))
            };
        }

        // All other methods from interface throwing System.NotSupportedException.
        #endregion

       
    }
}
