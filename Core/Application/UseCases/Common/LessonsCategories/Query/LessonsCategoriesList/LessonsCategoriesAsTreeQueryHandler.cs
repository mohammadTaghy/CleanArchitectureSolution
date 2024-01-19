using Application.Common.Model;
using AutoMapper;
using Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class LessonsCategoriesAsTreeQueryHandler :
        BaseLoadListQueryHandler<LessonsCategoriesAsTreeQuery, ILessonsCategoriesRepoRead, Common_LessonsCategories, LessonsCategoriesTreeDto>
    {


        public LessonsCategoriesAsTreeQueryHandler(ILessonsCategoriesRepoRead rolesLessonsCategoriesRepo, IMapper mapper, ICacheManager cacheManager) : base(rolesLessonsCategoriesRepo, mapper,cacheManager)
        {

        }

        public async Task<QueryResponse<List<LessonsCategoriesTreeDto>>> Handle(LessonsCategoriesAsTreeQuery request, CancellationToken cancellationToken)
        {


            Tuple<List<Common_LessonsCategories>, int> result = await _repo.ItemListAsTree(request.ODataQuery);
            if (result.Item2==0)
                return QueryResponse<List<LessonsCategoriesTreeDto>>.CreateInstance(new(), CommonMessage.Unauthorized, 0, false);

            return QueryResponse<List<LessonsCategoriesTreeDto>>.CreateInstance(_mapper.Map<List<LessonsCategoriesTreeDto>>(result.Item1), "", result.Item2, true);
        }
    }
}
