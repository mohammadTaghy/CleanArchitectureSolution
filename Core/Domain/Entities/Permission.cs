using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Permission : Entity
    {
        [Column(IsRequired = true, Title = "نام")]
        public string Name { get; set; }
        [Column(IsRequired = true, Title = "عنوان")]
        public string Title { get; set; }
        [Column(Title = "نام دستور")]
        public string CommandName { get; set; }
        [Column(Title = "نوع ویژگی")]
        public byte FeatureType { get; set; }
        [Column(IsRequired = true, Title = "فعال")]
        public bool IsActive { get; set; }
        [Column(IsRequired = true, Title = "کد رایانه پدر")]
        public int? ParentId { get; set; }
        [Column(IsRequired = true, Title = "مشخص کننده سطح")]
        public char LevelChar { get; set; }
        [Column(IsRequired = true, Title = "کد تولید شده توسط ماشین",Tooltip ="این کد نشان دهنده ترتیب ایتم در هر سطح می باشد")]
        public int AutoCode { get; set; }
        [Column(IsRequired = true, Title = "کد کامل ایجاد شده",Tooltip ="این کد نشان می دهد یک ایتم از چه شاخه ای می باشد")]
        public string FullKeyCode { get; set; }
        public ICollection<RolesPermission> RolesPermissions { get; set; }
        public Permission ParentEntity { get; set; }
        public ICollection<Permission> ChildList { get; set; }

    }
}
