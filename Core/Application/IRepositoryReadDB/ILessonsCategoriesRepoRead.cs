using Application.Common.Model;
using Application.UseCases;
using Domain.Entities;
using Microsoft.AspNetCore.OData.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface ILessonsCategoriesRepoRead : IRepositoryReadBase<Common_LessonsCategories>
    {
        Task<Tuple<List<Common_LessonsCategories>, int>> ItemListAsTree(ODataQueryOptions<Common_LessonsCategories> itemListParameter);
    }
}
