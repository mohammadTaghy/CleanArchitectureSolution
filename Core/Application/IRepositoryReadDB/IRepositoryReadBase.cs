using Domain;
using Microsoft.AspNetCore.OData.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IRepositoryReadBase<T>  where T : class,IEntity
    {
        #region Properties
        public CancellationToken CancellationToken { get; set; }
        #endregion
        #region Get
        IQueryable<T> Queryable { get; }
        Task<T> FindOne(int id);
        bool TryFindOne(int id, ref T? entity);
        T FindOne(Expression<Func<T, bool>> predicate);
        bool TryFindOne(Expression<Func<T, bool>> predicate, ref T? entity);
        IList<T> FindAll();
        IList<T> FindAll(Expression<Func<T, bool>> predicate);
        Task<Tuple<List<T>, int>> ItemList(ODataQueryOptions<T> options);
        bool Exists(Expression<Func<T, bool>> predicate);
        #endregion

        #region Manipulate
        Task Delete(T entity);
        Task Delete(int id);
        Task Delete(IList<T> entity);
        Task Delete(IList<int> ids);
        Task Add(T entity);
        Task AddMeny(IEnumerable<T> entities);
        Task Update(T entity);
        Task Update(IList<T> entities);
        #endregion

    }
}
