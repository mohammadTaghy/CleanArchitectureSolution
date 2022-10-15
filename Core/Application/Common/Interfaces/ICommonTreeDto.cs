using Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ICommonTreeDto
    {
        int Id { get; set; }

        int? ParentId { get; set; }
        bool HasChild { get; set; }

        List<ICommonTreeDto> ChildList { get; set; }
    }
}
