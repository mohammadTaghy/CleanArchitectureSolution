using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Model
{
    public class ItemListParameter
    {
        public string Filter { get; set; }
        public string Columns { get; set; }
        public int Top { get; set; }
        public int Skip { get; set; }
        public string Orderby { get; set; }
    }
}
