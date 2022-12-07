using Application.Common.Model;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public abstract class BaseLoadListQueryHandler<TRequest, TRepo, TEntity, TResponseEntity> :
        BaseCommandHandler<TRequest, QueryResponse<List<TResponseEntity>>, TRepo>
        where TRequest:BaseLoadListQuery<QueryResponse<List<TResponseEntity>>>, new()
        where TEntity : class,IEntity, new()
        where TRepo : IRepositoryBase<TEntity>
    {
        public BaseLoadListQueryHandler(TRepo tRepo, IMapper mapper) : base(tRepo, mapper)
        {

        }
        public override async Task<QueryResponse<List<TResponseEntity>>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            ItemListParameter parameter =new ItemListParameter
            {
                Columns=request.Columns,
                Filter=request.Filter,
                Orderby=request.Orderby,
                Skip=request.Skip,
                Top = request.Top   
            };
            Tuple<List<TEntity>,int> result= await _repo.ItemListAdo(parameter);
            List<TResponseEntity> entities= _mapper.Map<List<TResponseEntity>>(result.Item1);
            return QueryResponse<List<TResponseEntity>>.CreateInstance(entities, "", result.Item2);
        }
    }
}
