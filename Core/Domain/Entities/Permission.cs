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
        public ICollection<RolesPermission> RolesPermissions { get; set; }

    }
}
