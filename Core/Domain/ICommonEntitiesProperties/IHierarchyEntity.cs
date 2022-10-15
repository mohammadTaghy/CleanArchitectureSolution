using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IHierarchyEntity:IEntity
    {
        int? ParentId { get; set; }
        char LevelChar { get; set; }
        int AutoCode { get; set; }
        string FullKeyCode { get; set; }
    }
}
