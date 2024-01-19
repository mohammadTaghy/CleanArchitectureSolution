//using System.Net.Http;
//using System.Security.Claims;
//using System.Text.Json.Serialization;
//using Application.Common.Interfaces;
//using Application.UseCases;
//using Microsoft.AspNetCore.Http;
//using Newtonsoft.Json;

//namespace API.Services
//{
//    public class CurrentUserService : ICurrentUserService
//    {
//        private readonly IHttpContextAccessor _contextAccessor;
//        public CurrentUserService(IHttpContextAccessor httpContext)
//        {
//            UserId = httpContext.HttpContext.Session.GetInt32("UserId");
//            IsAuthenticated = UserId != null;
//            _contextAccessor = httpContext;
//        }

//        public int? UserId { get; }
//        public bool IsAuthenticated { get; }

        


//        public void SetUserId(int userId) => _contextAccessor.HttpContext.Session.SetInt32("UserId", userId);
        

//    }
//}
