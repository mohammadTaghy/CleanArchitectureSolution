using System.Security.Claims;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(HttpContext httpContext)
        {
            UserId = httpContext.Session.GetInt32("UserId");
            IsAuthenticated = UserId != null;
        }

        public int? UserId { get; }

        public bool IsAuthenticated { get; }
    }
}
