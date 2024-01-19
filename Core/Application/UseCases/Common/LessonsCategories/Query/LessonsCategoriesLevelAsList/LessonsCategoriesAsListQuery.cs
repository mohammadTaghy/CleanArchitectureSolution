using Application.Common.Model;
using Application.Mappings;
using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class LessonsCategoriesAsListQuery : BaseLoadListQuery<QueryResponse<List<LessonsCategoriesTreeDto>>,Common_LessonsCategories>
    {
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Common_LessonsCategories, LessonsCategoriesTreeDto>();
        }
    }
}
