using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class IMultilingual
    {
        [Column(Title = "کد رایانه زبان", IsVisible = true, IsRequired = false, IsReadOnly = true)]
        int CultureId { get; set; }
    }
}
