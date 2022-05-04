using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Column:Attribute
    {
        public Column()
        {
            IsVisible = true;
            IsRequired = false;
            IsReadOnly = false;
            LookupsName = string.Empty;
            Title = string.Empty;
            Tooltip = string.Empty;
            LookupType = null;
        }
        public string Title { get; set; }
        public string Tooltip { get; set; }
        public bool IsRequired { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsVisible { get; set; }
        public string LookupsName { get; set; }
        public Type? LookupType { get; set; }

    }
}
