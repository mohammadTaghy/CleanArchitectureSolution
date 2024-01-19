using Application.Common.Model;
using Domain;
using Microsoft.AspNetCore.OData.Query;

namespace Application.UseCases
{
    public interface IBaseLoadListQueryHandler<TRequest, TEntity, TResponseEntity>
        where TRequest : ODataQueryOptions<TEntity>, new()
        where TEntity : class, IEntity, new()
    {
        Task<QueryResponse<List<TResponseEntity>>> Handle(TRequest request, CancellationToken cancellationToken);
    }
}