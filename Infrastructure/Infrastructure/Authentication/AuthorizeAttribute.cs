using Common;
using Common.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;


namespace Infrastructure.Authentication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Token"];
            var returnResult = new HttpResponseMessage(HttpStatusCode.OK);
            if (string.IsNullOrEmpty(token))
            {
                // not logged in
                context.Result = new JsonResult(new { Message = CommonMessage.Unauthorized }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else
            {
                var tokenObject = JWTToken<TokenObject>.DecodeJson(token.First());
                if (tokenObject.UserType != 0)
                {
                    context.Result = new JsonResult(new { Message = CommonMessage.Unauthorized }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
        }
    }
}
