using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Common_LessonsCategories : HierarchyEntity<Common_LessonsCategories>,IEntity
    {
        public Common_LessonsCategories()
        {
            ParentId = null;
        }
        [Column(IsRequired = true, Title = "نام")]
        public string Name { get; set; }
        [Column(IsRequired = true, Title = "عنوان")]
        public string Title { get; set; }
        [Column(Title = "آدرس آیکن")]
        public string IConPath { get; set; }
        [Column(IsRequired = true, Title = "فعال")]
        public bool IsActive { get; set; }
        [Column(IsRequired = false, Title = "ضریب")]
        public byte Coefficient { get; set; }
        [Column(IsRequired = false, Title = "نوع دسته بندی")]
        public byte CategoryType { get; set; }
        public ICollection<Common_UserEducation> UserEducations { get; set; }

    }
}
