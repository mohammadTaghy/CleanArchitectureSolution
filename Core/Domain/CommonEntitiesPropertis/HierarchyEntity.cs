using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public abstract class HierarchyEntity<T>: Entity, IHierarchyEntity
        where T : class, IEntity
    {
        [Column(IsRequired = true, Title = "کد رایانه پدر")]
        public int? ParentId { get; set; }
        [Column(IsRequired = true, Title = "مشخص کننده سطح")]
        public char LevelChar { get; set; }
        [Column(IsRequired = true, Title = "کد تولید شده توسط ماشین", Tooltip = "این کد نشان دهنده ترتیب ایتم در هر سطح می باشد")]
        public int AutoCode { get; set; }
        [Column(IsRequired = true, Title = "کد کامل ایجاد شده", Tooltip = "این کد نشان می دهد یک ایتم از چه شاخه ای می باشد")]
        public string FullKeyCode { get; set; }

        public ICollection<T> ChildList { get; set; }
        public T ParentEntity { get; set; }

    }
}
