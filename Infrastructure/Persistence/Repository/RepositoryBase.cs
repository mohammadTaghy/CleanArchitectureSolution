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
    internal abstract class RepositoryBase<T> : IRepositoryBase<T>
        where T : class, IEntity
    {


        public RepositoryBase(PersistanceDBContext context)
        {
            Context = context;
        }

        #region Properties
        public CancellationToken CancellationToken { get; set; }
        public PersistanceDBContext Context;

        bool isNoTracking = false;
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
            if (entity is IChangeProperty)
            {
                IChangeProperty changeProperty = (IChangeProperty)entity;
                changeProperty.ModifyDate = DateTimeHelper.CurrentMDateTime;
                changeProperty.ModifyBy = CurrentUserSession.UserInfo.UserId;

            }
        }

        private void SetCreate(EntityEntry entity)
        {
            if (entity is ICreateProperty)
            {
                ICreateProperty changeProperty = (ICreateProperty)entity;
                changeProperty.CreateDate = DateTimeHelper.CurrentMDateTime;
                changeProperty.CreateBy = CurrentUserSession.UserInfo.UserId;

            }
        }
        private int SetID(EntityEntry entity)
        {
            int id = 0;
            if (entity is IEntity)
            {
                IEntity item = (IEntity)entity;
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
                    SetID(entity);
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
            return await GetAllAsQueryable().FirstAsync(x => x.Id == id, CancellationToken);
        }
        public T Find(Expression<Func<T, bool>> predicate)
        {
            return GetAllAsQueryable().First(predicate);
        }
        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await GetAllAsQueryable().FirstAsync(predicate, cancellationToken);
        }
        public async Task<IList<T>> ItemList(Expression<Func<T, bool>> predicate)
        {
            return await GetAllAsQueryable().Where(predicate).ToListAsync(CancellationToken);
        }

        #endregion
        #region Manipulate

        public virtual T DeleteItem(T entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            Save();
            return entity;
        }
        public virtual Task<T> DeleteItem(int id)
        {
            Task<T> entity = FindAsync(id);
            Context.Entry(entity).State = EntityState.Deleted;
            Save();
            return entity;
        }

        public virtual bool DeleteItems(IList<T> items)
        {
            foreach (T item in items)
            {
                Context.Entry(item).State = EntityState.Deleted;
            }
            Save();
            return true;
        }

        public virtual Task<bool> DeleteItems(IList<int> ids)
        {
            Task<IList<T>> items = ItemList(p => ids.Contains(p.Id));
            return items.ContinueWith(p=> DeleteItems(p.Result));
        }

        public virtual T Insert(T entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            Save();
            return entity;
        }
        public virtual T Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Save();
            return entity;
        }
        public void Add(T entity)
        {
            Detach();
            Context.Entry(entity).State = EntityState.Added;
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
        public async void Save()
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
