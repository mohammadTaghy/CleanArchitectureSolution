using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Common
{
    public interface ICurrentUserSession
    {
        int? UserId { get; }

        bool IsAuthenticated { get; }
    }
    public class CurrentUserSession: ICurrentUserSession
    {
        #region properties
        private  readonly IHttpContextAccessor _context;
        
        #endregion
        public CurrentUserSession(IHttpContextAccessor httpContext)
        {
            UserId = httpContext.HttpContext.Session.GetInt32("UserId");
            IsAuthenticated = UserId != null;
            _context = httpContext;
        }
        public int? UserId { get; }

        public bool IsAuthenticated { get; }




    }
    public class CurrentUserSessionDto
    {
        public CurrentUserSessionDto(int userId, Constants.YesNo isConfirm, string fullAddress, Constants.Gender gender, string fullName, string email,
            Constants.UserPermissionType userPermissionType, string userName, List<int> roleIds, string picturePath)
        {
            FullAddress = fullAddress;
            Gender = gender;
            UserId = userId;
            IsConfirm = isConfirm;
            FullAddress = fullAddress;
            FullName = fullName;
            Email = email;
            UserName = userName;
            RoleIds = roleIds;
            PicturePath = picturePath;
            UserPermissionType = userPermissionType;
        }
        public int UserId { get; init; }
        public Constants.YesNo IsConfirm { get; init; }
        public string FullAddress { get; init; }
        public Constants.Gender Gender { get; init; }
        public string FullName { get; init; }
        public string Email { get; init; }
        public Constants.UserPermissionType UserPermissionType { get; init; }
        public string UserName { get; init; }
        public List<int> RoleIds { get; init; }
        public string PicturePath { get; init; }
    }
}
