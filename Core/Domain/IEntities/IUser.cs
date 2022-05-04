using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    
    public interface IUser: IEntity, IChangeProperty, ICreateProperty
    {
        [Column(IsRequired = true,Title ="نام کاربری")]
        string UserName { get; set; }
        [Column(IsRequired = true, Title = "نام کاربری")]
        int UserCode { get; set; }
        [Column(IsRequired = true, Title = "نام")]
        string FirstName { get; set; }
        [Column(IsRequired = true, Title = "نام خانوادگی")]
        string LastName { get; set; }
        [Column(Title ="کد ملی")]
        string NationalCode { get; set; }
        [Column(Title = "شماره موبایل")]
        string MobileNumber { get; set; }

    }
}
