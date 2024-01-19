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
            MaxLength=int.MaxValue;
            MinLength=int.MinValue;
            Length= int.MaxValue;
        }
        public string Title { get; set; }
        public string Tooltip { get; set; }
        public bool IsRequired { get; set; }
        public bool IsReadOnly { get; set; }
        public int MaxLength { get; set; }
        public int MinLength { get; set; }  
        public int Length { get; set; }
        public int LessThan { get; set; }
        public int GreaterThan { get; set; }
        public bool IsVisible { get; set; }
        public string LookupsName { get; set; }
        public Type? LookupType { get; set; }

    }
}
