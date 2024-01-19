using Application.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class LessonsCategoriesTreeDto : CommonTreeDto<LessonsCategoriesTreeDto>
    {
        public string Name { get; set; }
        public string Title { get; set; }
    }
}
