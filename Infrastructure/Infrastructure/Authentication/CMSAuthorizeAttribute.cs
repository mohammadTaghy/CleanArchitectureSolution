using Application;
using Application.Common.Interfaces;
using Application.UseCases;
using Application.UseCases.Membership.UserCase;
using Common;
using Common.JWT;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;
using System.Security.Claims;
using System.Text.Json.Nodes;
using static Common.Constants;

namespace Infrastructure.Authentication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute(params string[] claim) : base(typeof(AuthorizeFilter))
        {
            Arguments = new object[] { claim };
        }
    }
    public class AuthorizeFilter(IUserRepoRead userRepoRead, params string[] claim) : IAuthorizationFilter
    {
        private readonly IUserRepoRead userRepo = userRepoRead;
        readonly string[] _claim = claim;

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            var IsAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;

            if (IsAuthenticated)
            {
                bool flagClaim = false;
                var claimsIndentity = context.HttpContext.User.Identity as ClaimsIdentity;
                int userId = int.Parse(claimsIndentity.Claims.First(p => p.Type == nameof(TokenClaimType.UserId)).Value);
                Membership_User user = await userRepo.FindOne(userId);
                foreach (var item in _claim)
                {
                    if (context.HttpContext.User.HasClaim(item, item))
                        flagClaim = true;
                }
                if (!flagClaim)
                    context.Result = new RedirectResult("~/Dashboard/NoPermission");
            }
            else
            {
                context.Result = new RedirectResult("~/Home/Index");
            }
            return;
        }
    }


}
