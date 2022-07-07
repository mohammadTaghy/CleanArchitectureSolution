
namespace Domain.Entities
{
    
    public class User : Entity
    {
        [Column(IsRequired = true, Title = "نام کاربری")]
        public string UserName { get; set; }
        [Column(IsRequired = true, Title = "کد کاربر")]
        public string? UserCode { get; set; }
        [Column( Title = "شماره همراه")]
        public string MobileNumber { get ; set ; }
        [Column(IsRequired = true, Title = "پسوورد")]
        public string PasswordHash { get ; set ; }
        [Column(IsRequired = true, Title = "شماره همراه تایید شده است")]
        public bool IsMobileNumberConfirmed { get ; set ; }
        [Column( Title = "ایمیل")]
        public string Email { get ; set ; }
        [Column(IsRequired = true, Title = "ایمیل تایید شده است")]
        public bool IsEmailConfirmed { get ; set ; }
        [Column(IsRequired = true, Title = "کاربر خود را تایید کرده است")]
        public bool IsUserConfirm { get ; set ; }
        [Column(IsRequired = true, Title = "مدیر کاریر را تایید کرده است")]
        public byte ManagerConfirm { get ; set ; }
        [Column(IsRequired = true, Title = "شناسه دستگاه")]
        public string DeviceId { get ; set ; }
        public UserProfile UserProfile { get ; set ; }
        public ICollection<UserRoles> UserRoles { get ; set ; }
    }
    
}
