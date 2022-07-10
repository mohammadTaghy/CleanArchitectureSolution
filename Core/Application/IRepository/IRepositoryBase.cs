using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IRepositoryBase<T> where T : class, IEntity
    {
        #region Properties
        public CancellationToken CancellationToken { get; set; }
        #endregion
        #region Get
        IQueryable<T> GetAllAsQueryable();
        IQueryable<T> GetAllAsQueryable(string[] includeList);
        T Find(int id);
        Task<T> FindAsync(int id);
        T Find(Expression<Func<T, bool>> predicate);
        Task<T> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<IList<T>> ItemList(Expression<Func<T, bool>> predicate);
        Task<bool> AnyEntity(Expression<Func<T, bool>> predicate);
        #endregion
        #region Manipulate
        void Add(T entity);
        void Attach(T entity);
        Task<T> Insert(T entity);
        Task<T> Update(T entity);
        Task<T> DeleteItem(T entity);
        Task<T> DeleteItem(int id);
        Task<bool> DeleteItems(IList<T> items);
        bool DeleteItems(IList<int> ids);
        Task Save();
        #endregion
    }
}
