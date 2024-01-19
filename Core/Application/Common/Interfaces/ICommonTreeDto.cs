using Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ICommonTreeDto<E>
        where E : class, ICommonTreeDto<E>, new()
    {
        int Id { get; set; }

        int? ParentId { get; set; }
        bool HasChild { get; set; }

        List<E> ChildList { get; set; }
    }
}
