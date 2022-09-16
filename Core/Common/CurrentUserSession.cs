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
    public class CurrentUserSession
    {
        #region properties
        private static CurrentUserSessionDto _userInfo = null;
        private  readonly HttpContext _context;
        public static CurrentUserSessionDto UserInfo
        {
            get
            {
                //if (_userInfo == null)
                //    _userInfo = JsonConvert.DeserializeObject<CurrentUserSessionDto>(
                //        _context.Session.GetString("UserInfo"));
                return _userInfo;

            }
            set { _userInfo = value; }
        }
        #endregion
        public CurrentUserSession(HttpContext context)
        {
            _context = context;
        }
        //public static void AddUserIfoSession(CurrentUserSessionDto userSessionDto)
        //{
        //    _context.Session.SetString("userInfo", JsonConvert.SerializeObject(userSessionDto));
        //}
       

       

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
