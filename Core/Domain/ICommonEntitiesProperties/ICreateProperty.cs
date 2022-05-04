using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface ICreateProperty
    {
        [Column(Title = "ایجاد کننده", IsReadOnly = true)]
        int CreateBy { get; set; }
        [Column(Title = "تاریخ ایجاد", IsReadOnly = true)]
        System.DateTime CreateDate { get; set; }
    }
}
