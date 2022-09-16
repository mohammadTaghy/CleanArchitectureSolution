using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class PermissionTreeDto
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public bool HasChild { get; set; }
        public string Title { get; set; }
        public bool HasPermission { get; set; }
        public List<PermissionTreeDto> ChildList { get; set; }
    }
}
