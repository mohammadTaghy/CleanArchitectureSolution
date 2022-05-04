using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IChangeProperty
    {
        [Column(Title ="تغییر دهنده",IsReadOnly =true)]
        int? ModifyBy { get; set; }
        [Column(Title = "تاریخ تغییر", IsReadOnly = true)]

        DateTime? ModifyDate { get; set; }
    }
}
