using Common;
using Common.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication
{
    public class JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings, IJWTTokenHelper jWTTokenHelper)
    {
        private readonly RequestDelegate _next = next;
        private readonly IJWTTokenHelper jWTTokenHelper = jWTTokenHelper;
        private readonly AppSettings _appSettings = appSettings.Value;

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault();
            attachUserToContext(context, token);
            //if (token == null)
            //    throw new Exception(CommonMessage.Unauthorized);

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, string token)
        {
            try
            {
                
                var jwtToken = jWTTokenHelper.DecodeJson(token.Replace("Bearer","",true, null).Trim());
                var userId = jwtToken.First(x => x.Type == nameof(Common.Constants.TokenClaimType.UserId)).Value;
                // attach user to context on successful jwt validation
                var identity = new ClaimsIdentity(new List<Claim>
                    {
                        new Claim("UserId", userId, ClaimValueTypes.Integer32)
                    }, "Custom");
                context.User = new ClaimsPrincipal(identity);
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
