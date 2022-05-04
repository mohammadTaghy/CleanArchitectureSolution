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
        #endregion
        #region Manipulate
        void Add(T entity);
        void Attach(T entity);
        T Insert(T entity);
        T Update(T entity);
        T DeleteItem(T entity);
        Task<T> DeleteItem(int id);
        bool DeleteItems(IList<T> items);
        Task<bool> DeleteItems(IList<int> ids);
        void Save();
        #endregion
    }
}
