using Common.Extention;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Constants
    {
        public enum UserPermissionType
        {
            CmsAdministrator,
            CmsUser,
            FrontAdministrator,
            FrontUser,
        }
        public enum GeoLocationType
        {
            Country,
            Province,
            City,
            Mahale,
        }
        public enum YesNo
        {
            No,
            Yes

        }
        public enum Gender
        {
            [EnumDisplayName(DisplayName ="آقا")]
            Male,
            [EnumDisplayName(DisplayName = "خانم")]
            Female
        }
        public enum Status:byte
        {
            [EnumDisplayName(DisplayName = "در حال بررسی")]
            InCheck,
            [EnumDisplayName(DisplayName = "تایید شده")]
            Confirm,
            [EnumDisplayName(DisplayName = "تایید نشده")]
            UnConfirm
        }
    }
}
