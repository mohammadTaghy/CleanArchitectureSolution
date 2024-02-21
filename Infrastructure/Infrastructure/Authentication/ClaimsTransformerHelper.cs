using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Infrastructure.Authentication
{
    public class ClaimsTransformerHelper(IIdentityStoreHelper identityStoreHelper) : IClaimsTransformation
    {
        private readonly IIdentityStoreHelper identityStoreHelper = identityStoreHelper;

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal.Identity.IsAuthenticated)
            {
                ClaimsIdentity claimsIdentity = new ClaimsIdentity();
                int userId = int.Parse(principal.Claims.First(p => p.Type == nameof(Common.Constants.TokenClaimType.UserId)).Value);
                var claims= await identityStoreHelper.GetClaimsAsync(userId,CancellationToken.None);
                claimsIdentity.AddClaims(claims);
                principal.AddIdentity(claimsIdentity);
                return await Task.FromResult(principal);
            }
            return await Task.FromResult(principal);
        }
    }
}
