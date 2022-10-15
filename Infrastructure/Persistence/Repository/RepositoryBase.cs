using Application;
using Common;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
        where T : class, IEntity
    {
        private readonly ICurrentUserSession _currentUserSession;


        public RepositoryBase(PersistanceDBContext context, ICurrentUserSession currentUserSession)
        {
            Context = context;
            _currentUserSession = currentUserSession;
        }

        #region Properties
        public CancellationToken CancellationToken { get; set; }
        public PersistanceDBContext Context;
        bool isNoTracking = true;
        int maxId = 0;
        int MaxId
        {
            get
            {
                if (maxId == 0)
                    maxId = (GetAllAsQueryable().Max(x => (int?)x.Id) ?? 0);
                return maxId;
            }
            set { maxId = value; }
        }
        #endregion
        #region CustomMethod
        private void SetModify(EntityEntry entity)
        {
            if (entity.Entity is IChangeProperty)
            {
                IChangeProperty changeProperty = (IChangeProperty)entity.Entity;
                changeProperty.ModifyDate = DateTimeHelper.CurrentMDateTime;
                changeProperty.ModifyBy = _currentUserSession.UserId;

            }
        }

        private void SetCreate(EntityEntry entity)
        {
            if (entity.Entity is ICreateProperty)
            {
                ICreateProperty changeProperty = (ICreateProperty)entity.Entity;
                changeProperty.CreateDate = DateTimeHelper.CurrentMDateTime;
                changeProperty.CreateBy = _currentUserSession.UserId??0;

            }
        }
        private int SetID(EntityEntry entity)
        {
            int id = 0;
            if (entity.Entity is IEntity)
            {
                IEntity item = (IEntity)entity.Entity;
                MaxId++;
                id = MaxId;
            }
            return id;
        }
        protected void SetNoTracking(bool noTraking) => isNoTracking = noTraking;
        private void FillEntityProperty(EntityEntry entity)
        {
            try
            {
                if (entity.State == EntityState.Added)
                {
                   // SetID(entity);
                    SetCreate(entity);

                }
                else if (entity.State == EntityState.Modified)
                {
                    SetModify(entity);
                }

            }

            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
        #region Get
        public IQueryable<T> GetAllAsQueryable()
        {
            IQueryable<T> query = from p in Context.Set<T>()
                                  select p;
            if (isNoTracking)
                query = query.AsNoTracking();
            return query;
        }

        public IQueryable<T> GetAllAsQueryable(string[] includeList)
        {
            var Query = GetAllAsQueryable();
            if (includeList != null)
                foreach (string include in includeList)
                    Query = Query.Include(include);
            return Query;
        }
        public T Find(int id)
        {
            return GetAllAsQueryable().First(x => x.Id == id);
        }
        public async Task<T> FindAsync(int id)
        {
            return await GetAllAsQueryable().FirstOrDefaultAsync(x => x.Id == id, CancellationToken);
        }
        public T Find(Expression<Func<T, bool>> predicate)
        {
            return GetAllAsQueryable().FirstOrDefault(predicate);
        }
        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await GetAllAsQueryable().FirstOrDefaultAsync(predicate, cancellationToken);
        }
        public async Task<IList<T>> ItemList(Expression<Func<T, bool>> predicate)
        {
            return await GetAllAsQueryable().Where(predicate).ToListAsync(CancellationToken);
        }
        public async Task<bool> AnyEntity(Expression<Func<T, bool>> predicate)
        {
            return await GetAllAsQueryable().AnyAsync(predicate);
        }
        #endregion
        #region Manipulate

        public virtual async Task<bool> DeleteItem(T entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
             await  Save();
            return true;
        }
        public virtual async Task DeleteItem(int id)
        {
            Task<T> entity = FindAsync(id);
            Context.Entry(entity).State = EntityState.Deleted;
            await Save();
        }

        public virtual async Task<bool> DeleteItems(IList<T> items)
        {
            foreach (T item in items)
            {
                Context.Entry(item).State = EntityState.Deleted;
            }
            await Save();
            return true;
        }

        public virtual bool DeleteItems(IList<int> ids)
        {
            IList<T> items = ItemList(p => ids.Contains(p.Id)).Result;
            return DeleteItems(items).Result;
        }

        public virtual async Task Insert(T entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            await Save();
        }
        public virtual async Task Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Save();
        }
        public void Add(T entity)
        {
            Detach();
            Context.Entry(entity).State = EntityState.Added;
        }
        public void Delete(T entity)
        {
            Detach();
            Context.Entry(entity).State = EntityState.Deleted;
        }
        public void Attach(T entity)
        {
            Detach();
            Context.Entry(entity).State = EntityState.Modified;
        }
        public void Detach()
        {
            var changedEntriesCopy = Context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Unchanged).ToList();
            Task.Run(() =>
            {
                Parallel.ForEach<EntityEntry>(changedEntriesCopy, entity =>
                 {
                     entity.State = EntityState.Detached;
                 });
            });
        }
        public async Task Save()
        {
            var entities = Context.ChangeTracker.Entries().Where(p => p.State != EntityState.Unchanged);
           // await Task.Run(() =>
           //{
               Parallel.ForEach(entities, entity =>
                {
                    FillEntityProperty(entity);
                });
           //});

            try
            {
                await Context.SaveChangesAsync(CancellationToken);
            }

            catch (Exception ex)
            {
                //must be log
                var error = ex.GetBaseException();
                throw;
            }

        }
        #endregion

    }
}
