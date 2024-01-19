using Application;
using Application.Common.Model;
using Application.UseCases;
using Common;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class LessonsCategoriesRepo : HierarchyEntityRepo<Common_LessonsCategories>, ILessonsCategoriesRepo
    {
        public LessonsCategoriesRepo(PersistanceDBContext context, ICurrentUserSession currentUserSession) : base(context, currentUserSession)
        {
        }

    }
}
