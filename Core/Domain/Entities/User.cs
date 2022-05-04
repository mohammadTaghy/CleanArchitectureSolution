
namespace Domain
{
    
    public class User : Entity, IUser
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserCode { get; set; }
        public string NationalCode { get ; set ; }
        public string MobileNumber { get ; set ; }
    }
    
}
