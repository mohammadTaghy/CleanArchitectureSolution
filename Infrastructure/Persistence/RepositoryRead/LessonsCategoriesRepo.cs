using Application;
using Application.Common;
using Application.Common.Model;
using Application.UseCases;
using Common;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Persistence.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class LessonsCategoriesRepoRead : HierarchyEntityRepoRead<Common_LessonsCategories>, ILessonsCategoriesRepoRead
    {
        public LessonsCategoriesRepoRead(IOptions<MongoDatabaseOption> config, IDirectExchangeRabbitMQ directExchangeRabbitMQ) : base(config, directExchangeRabbitMQ)
        {
        }
        #region CustomGet
        public async Task<Tuple<List<Common_LessonsCategories>, int>> ItemListAsTree(ODataQueryOptions<Common_LessonsCategories> oDataQueryOptions)
        {
            return await ItemList(oDataQueryOptions);
            //List<LessonsCategoriesTreeDto> lessonsCategoriesTreeDto = results.Item1.Select(p => new LessonsCategoriesTreeDto
            //{
            //    Id = p.Id,
            //    Title = p.Title,
            //    Name = p.Name,
            //    ParentId = p.ParentId
            //}).ToList();
            //List<LessonsCategoriesTreeDto> parents = lessonsCategoriesTreeDto.Where(p => p.ParentId == null).ToList();
            //ChangeToHierarchy(parents, lessonsCategoriesTreeDto.Except(parents).ToList());
            //return new Tuple<List<LessonsCategoriesTreeDto>, int>(parents, results.Item2);
        }
        #endregion

    }
}
