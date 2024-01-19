using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class CommonMessage
    {
        public const string RequiredMessage = "مقدار داده {0} اجباری می باشد";
        public const string NotEmpty = "مقدار داده {0} نمی تواند خالی باشد";
        public const string MinimumLength = "طول {0} کافی نیست";
        public const string NotFound = "اطلاعاتی برای '{0}' یافت نشد";
        public const string SucceedUpdate = "اطلاعات '{0}' با موفقیت ذخیره شد";
        public const string SucceedData = "اطلاعات موجود برای '{0}'";
        public const string DeleteFailure = "حذف اینتیتی \"{0}\" ({1}) .نا موفق بود ";
        public const string IsDuplicateUserName = "نام کاربری {0} تکراری می باشد";

        public const string Error = "خطایی وجود دارد";

        public const string ValidationMessage = "یک یا چند اعتبارسنجی با مشکل مواجه شده است";

        public const string NullException = "مقدار پارامتر {0} برابر null می باشد ";
        public const string Unauthorized="شما دسترسی ندارید";

        public const string AccessDenied = "";
        public const string IsDuplicate= "مقدار {0} تکراری می باشد";

        public const string EmptyResponse = "داده ای تعریف نشده است";

        public const string BadRequestException = "داده ها یا ادرس ارسالی اشتباه است";

        public const string HasConflict = "داده ای که قصد حذف آن را دارید در {0} داده دارد";
    }
}
