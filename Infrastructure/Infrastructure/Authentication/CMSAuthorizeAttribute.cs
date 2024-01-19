using Application.Common.Interfaces;
using Common;
using Common.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Nodes;

namespace Infrastructure.Authentication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CMSAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _permissionName;

        public CMSAuthorizeAttribute( string permissionName="") {
            _permissionName = permissionName;
        }
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
                var currentUserSession = IOCManager.GetService<ICurrentUserSession>();
                var tokenObject = JWTToken<BaseJwtPayload>.DecodeJson(token.First());
                if (tokenObject!=null && (!tokenObject.Any(p => p.Type == nameof(BaseJwtPayload.UserId)) 
                    || int.Parse(tokenObject.First(p=>p.Type == nameof(BaseJwtPayload.UserId)).Value)!= currentUserSession.UserId))
                    context.Result = new JsonResult(new { Message = CommonMessage.Unauthorized }) { StatusCode = StatusCodes.Status401Unauthorized };
                if(!string.IsNullOrEmpty(_permissionName) && !currentUserSession.Permissions.Any(p=>p==_permissionName))
                    context.Result = new JsonResult(new { Message = CommonMessage.Unauthorized }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }

       
    }
}
