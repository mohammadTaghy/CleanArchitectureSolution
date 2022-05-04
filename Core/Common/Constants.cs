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
            Male,
            Female
        }
    }
}
