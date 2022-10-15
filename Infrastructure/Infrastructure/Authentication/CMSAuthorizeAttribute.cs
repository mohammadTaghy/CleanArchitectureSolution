using Application.Common.Interfaces;
using Common;
using Common.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MongoDB.Bson.IO;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Nodes;

namespace Infrastructure.Authentication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CMSAuthorizeAttribute : Attribute, IAuthorizationFilter
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
                var tokenObject = JWTToken<BaseJwtPayload>.DecodeJson(token.First());
                if (tokenObject!=null && (!tokenObject.Any(p => p.Type == nameof(BaseJwtPayload.UserId)) 
                    || int.Parse(tokenObject.First(p=>p.Type == nameof(BaseJwtPayload.UserId)).Value)!=context.HttpContext.Session.GetInt32("UserId")))
                {
                    context.Result = new JsonResult(new { Message = CommonMessage.Unauthorized }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
        }
    }
}
