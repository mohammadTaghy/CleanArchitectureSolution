using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IModular
    {
        [Column(Title ="کد رایانه ماژول",IsVisible =true,IsRequired =false,IsReadOnly =true)]
        int ModuleId { get; set; }
    }
}
