using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UserCase.Command
{
    public abstract class UserCommandBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsMobileNumberConfirmed { get; set; } = false;
        public bool IsUserConfirm { get; set; } = false;
        public byte Gender { get; set; }
        public string? PostalCode { get; set; }
        public string? PicturePath { get; set; }
        public string BirthDate { get; set; }
        public string? EducationGrade { get; set; }
        public string? UserDescription { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? NationalCode { get; set; }

    }
}
