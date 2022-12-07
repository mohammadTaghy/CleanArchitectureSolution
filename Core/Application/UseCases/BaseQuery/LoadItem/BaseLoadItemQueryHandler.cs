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
    public abstract class BaseLoadItemQueryHandler<TRequest, TRepo, TEntity, TResponseEntity> :
        BaseCommandHandler<TRequest, QueryResponse<TResponseEntity>, TRepo>
        where TRequest: BaseLoadItemQuery<QueryResponse<TResponseEntity>>, new()
        where TEntity : class,IEntity, new()
        where TRepo : IRepositoryBase<TEntity>
        where TResponseEntity:class,new()
    {
        public BaseLoadItemQueryHandler(TRepo tRepo, IMapper mapper) : base(tRepo, mapper)
        {

        }
        public override async Task<QueryResponse<TResponseEntity>> Handle(TRequest request, CancellationToken cancellationToken)
        {
           
            TEntity result= await _repo.FindAsync(request.Id);
            TResponseEntity entityDto= _mapper.Map<TResponseEntity>(result);
            return QueryResponse<TResponseEntity>.CreateInstance(entityDto, "", 1);
        }
    }
}
