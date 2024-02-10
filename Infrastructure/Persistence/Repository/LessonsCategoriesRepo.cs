using Application;
using Common;
using Domain.Entities;

namespace Persistence.Repository
{
    public class LessonsCategoriesRepo : HierarchyEntityRepo<Common_LessonsCategories>, ILessonsCategoriesRepo
    {
        public LessonsCategoriesRepo(PersistanceDBContext context, ICurrentUserSession currentUserSession) : base(context, currentUserSession)
        {
        }
       
    }
}
