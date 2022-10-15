using Application.Common.Interfaces;
using Application.Common.Model;
using Application.UseCases;
using Common;
using Domain;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class HierarchyEntityRepo<T> : RepositoryBase<T>
        where T : class, IHierarchyEntity
    {
        public HierarchyEntityRepo(PersistanceDBContext context, ICurrentUserSession currentUserSession) : base(context,currentUserSession)
        {
        }
        #region HierarchyMethod
        protected void ChangeToHierarchy<E>(List<E> parents, List<E> childs) where E : class, ICommonTreeDto
        {
            List<int> parentIds = new List<int>();
            List<E> nextLevel = new List<E>();
            parents.ForEach(p =>
            {
                if (p.ChildList == null)
                    p.ChildList = new();
                p.HasChild = childs.Count > 0;
                p.ChildList.AddRange(childs.Where(q => q.ParentId == p.Id));
                parentIds.Add(p.Id);
                nextLevel.AddRange(childs.Where(q => q.ParentId == p.Id));
            });
            var othersChild = childs.Except(nextLevel).ToList();
            if (othersChild.Any())
                ChangeToHierarchy(nextLevel, othersChild);

        }
        protected int GetMaxAutoCode(int? parentId, char level)
        {
            int maxAutoCode = 0;
            if (parentId != null)
                maxAutoCode = GetAllAsQueryable().Any(p => p.ParentId == parentId) ?
                    GetAllAsQueryable().Where(p => p.ParentId == parentId).Max(p => p.AutoCode) + 1 :
                    100;
            else
                maxAutoCode = GetAllAsQueryable().Any(p => p.LevelChar == level) ?
                GetAllAsQueryable().Where(p => p.LevelChar == level).Max(p => p.AutoCode) + 1 :
                100;
            return maxAutoCode;

        }
        #endregion
    }
}
