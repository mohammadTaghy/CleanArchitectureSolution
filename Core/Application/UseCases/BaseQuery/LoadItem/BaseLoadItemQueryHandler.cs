using Application.Common.Exceptions;
using Application.Common.Model;
using AutoMapper;
using Common;
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
        where TRepo : IRepositoryReadBase<TEntity>
        where TResponseEntity:class,new()
    {
        public BaseLoadItemQueryHandler(TRepo tRepo, IMapper mapper, ICacheManager cacheManager) : base(tRepo, mapper, cacheManager)
        {

        }
        public override async Task<QueryResponse<TResponseEntity>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            if(request is null) throw new BadRequestException(string.Format(CommonMessage.NullException, nameof(request)));
            TEntity result= await _repo.FindOne(request.Id);
            if (result == null)
                throw new NotFoundException(request.Id.ToString(), request.Id);
            TResponseEntity entityDto= _mapper.Map<TResponseEntity>(result);
            return QueryResponse<TResponseEntity>.CreateInstance(entityDto, "", 1);
        }
    }
}
