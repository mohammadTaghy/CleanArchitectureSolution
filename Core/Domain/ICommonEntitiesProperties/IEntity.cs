using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IEntity
    {
        [Column(Title ="کد رایانه",IsReadOnly =true,Tooltip ="کد رایانه توسط سیستم تعیین می شود")]
        int Id { get; set; }
        
    }
}
