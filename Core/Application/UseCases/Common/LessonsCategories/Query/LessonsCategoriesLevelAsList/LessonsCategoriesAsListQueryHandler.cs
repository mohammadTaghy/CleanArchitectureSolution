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
    public class LessonsCategoriesAsListQueryHandler :
        BaseLoadListQueryHandler<LessonsCategoriesAsListQuery, ILessonsCategoriesRepoRead, Common_LessonsCategories, LessonsCategoriesTreeDto>
    {
        public LessonsCategoriesAsListQueryHandler(ILessonsCategoriesRepoRead rolesLessonsCategoriesRepo, IMapper mapper, ICacheManager cacheManager) : base(rolesLessonsCategoriesRepo, mapper,cacheManager)
        {

        }
    }
}
