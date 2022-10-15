using Application.Common.Interfaces;
using Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Model
{
    public class CommonTreeDto: ICommonTreeDto
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }
        public bool HasChild { get; set; }

        public List<ICommonTreeDto> ChildList { get; set; }

    }
}
