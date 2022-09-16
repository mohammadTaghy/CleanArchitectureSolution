using Application;
using Application.UseCases;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class PermissionRepo : RepositoryBase<Permission>, IPermissionRepo
    {

        public PermissionRepo(PersistanceDBContext context) : base(context)
        {
           
        }
        public override Task Insert(Permission entity)
        {
            if(entity.ParentId!=null)
            {
                Permission parentPermission = base.Find(entity.ParentId.Value);
                entity.LevelChar = (char)((int)parentPermission.LevelChar + 1);
                entity.AutoCode = GetMaxAutoCode(entity.ParentId,entity.LevelChar);
                entity.FullKeyCode = parentPermission.FullKeyCode + entity.LevelChar + entity.AutoCode;
            }
            else
            {
                entity.LevelChar ='a';
                entity.AutoCode = GetMaxAutoCode(entity.ParentId,'a');
                entity.FullKeyCode = entity.LevelChar + entity.AutoCode.ToString();
            }
            return base.Insert(entity);
        }

        private int GetMaxAutoCode(int? parentId, char level)
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
    }
}
