using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Membership_Locations : HierarchyEntity<Membership_Locations>
    {
        public Membership_Locations()
        {
            ParentId= null;
        }
        [Column(IsRequired = true, Title = "نام")]
        public string Name { get; set; }
        [Column(IsRequired = true, Title = "عنوان")]
        public string Title { get; set; }
        [Column(Title = "آدرس آیکن")]
        public string IConPath { get; set; }
        [Column(IsRequired = true, Title = "فعال")]
        public bool IsActive { get; set; }
        [Column(IsRequired = false, Title = "کد پیش شماره")]
        public bool NumberCode { get; set; }
    }
}
