using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserProfile : User
    {
        [Column(IsRequired = true, Title = "جنسیت")]
        public byte Gender { get; set; }
        [Column(IsRequired = true, Title = "کد پستی")]
        public string? PostalCode { get; set; }
        [Column(Title = "تصویر")]
        public string? PicturePath { get; set; }
        [Column( Title = "تاریخ تولد")]
        public DateTime? BirthDate { get; set; }
        [Column(IsRequired = true, Title = "مدرک تحصیلی")]
        public string? EducationGrade { get; set; }
        [Column(Title = "شرح کاربر از خود")]
        public string? UserDescription { get; set; }
        [Column(IsRequired = true, Title = "نام")]
        public string FirstName { get; set; }
        [Column(IsRequired = true, Title = "نام خانوادگی")]
        public string LastName { get; set; }
        [Column( Title = "مد ملی")]
        public string? NationalCode { get; set; }
    }
}
