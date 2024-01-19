using Application.Common.Model;
using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class LessonsCategoriesAsTreeQuery : BaseLoadListQuery<QueryResponse<List<LessonsCategoriesTreeDto>>,Common_LessonsCategories>
    {
        
    }
}
