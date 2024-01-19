using Application.Common.Interfaces;
using Application.Common.Model;
using Application.UseCases;
using Common;
using Domain;
using Domain.Entities;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class HierarchyEntityRepo<T> : RepositoryBase<T>
        where T : class, IHierarchyEntity<T>
    {
        public HierarchyEntityRepo(PersistanceDBContext context, ICurrentUserSession currentUserSession) : base(context,currentUserSession)
        {
        }
        #region HierarchyMethod
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
        #region Manipulate
        public override Task Insert(T entity)
        {
            if (entity.ParentId != null && entity.ParentId != 0)
            {
                T parentPermission = base.Find(entity.ParentId.Value);
                entity.LevelChar = (char)((int)parentPermission.LevelChar + 1);
                entity.AutoCode = GetMaxAutoCode(entity.ParentId, entity.LevelChar);
                entity.FullKeyCode = parentPermission.FullKeyCode + entity.LevelChar + entity.AutoCode;
            }
            else
            {
                entity.ParentId = null;
                entity.LevelChar = 'a';
                entity.AutoCode = GetMaxAutoCode(entity.ParentId, 'a');
                entity.FullKeyCode = entity.LevelChar + entity.AutoCode.ToString();
            }
            return base.Insert(entity);
        }
        #endregion
    }
}
