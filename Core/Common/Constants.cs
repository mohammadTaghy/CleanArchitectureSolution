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
            [EnumDisplayName(DisplayName = "آقا")]
            Male,
            [EnumDisplayName(DisplayName = "خانم")]
            Female
        }
        public enum Status : byte
        {
            [EnumDisplayName(DisplayName = "در حال بررسی")]
            InCheck,
            [EnumDisplayName(DisplayName = "تایید شده")]
            Confirm,
            [EnumDisplayName(DisplayName = "تایید نشده")]
            UnConfirm
        }
        public enum FeatureType
        {
            Menu,
            Form,
            Tab,
            Command
        }
        public enum LessonsCategoriesType
        {
            [EnumDisplayName(DisplayName = "مقطع تحصیلی")]
            Grade,
            [EnumDisplayName(DisplayName = "پایه تحصیلی")]
            Course,
            [EnumDisplayName(DisplayName = "کتاب درسی")]
            Book,
            [EnumDisplayName(DisplayName = "بخش")]
            Section,
            [EnumDisplayName(DisplayName = "درس")]
            Lesson
        }
        public enum FileType
        {
            PDF = 1,
            DOCX ,
            Image,
            Movie,
            Sound
        }
        public enum ChangedType : byte
        {
            Create,
            Update,
            Delete
        }
        public static string GetGenderString(byte gender)
        {
            switch (gender)
            {
                case (byte)Constants.Gender.Male:
                    return Constants.Gender.Male.DisplayName();
                case (byte)Constants.Gender.Female:
                    return Constants.Gender.Female.DisplayName();
                default:
                    return Constants.Gender.Male.DisplayName();
            }
        }
        public enum TokenClaimType
        {
            UserId
        }
    }
}
