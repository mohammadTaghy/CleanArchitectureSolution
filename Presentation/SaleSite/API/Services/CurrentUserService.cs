using System.Security.Claims;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = 1;
            IsAuthenticated = UserId != null;
        }

        public int? UserId { get; }

        public bool IsAuthenticated { get; }
    }
}
