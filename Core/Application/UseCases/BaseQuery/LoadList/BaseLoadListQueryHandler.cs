using Application.Common.Exceptions;
using Application.Common.Model;
using AutoMapper;
using Common;
using Domain;
using MediatR;
using Microsoft.AspNetCore.OData.Query;
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
        IRequestHandler<TRequest, QueryResponse<List<TResponseEntity>>>
        where TRequest : BaseLoadListQuery<QueryResponse<List<TResponseEntity>>, TEntity>, new()
        where TEntity : class, IEntity, new()
        where TRepo : IRepositoryReadBase<TEntity>
    {
        protected readonly TRepo _repo;
        protected readonly IMapper _mapper;
        protected readonly ICacheManager _cacheManager;

        public BaseLoadListQueryHandler(TRepo repo, IMapper mapper, ICacheManager cacheManager)
        {
            _repo = repo;
            _mapper = mapper;
            _cacheManager = cacheManager;
        }


        public async Task<QueryResponse<List<TResponseEntity>>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            if (request == null) throw new BadRequestException(string.Format(CommonMessage.NullException, nameof(request)));

            Tuple<List<TEntity>, int> result = await _repo.ItemList(request.ODataQuery);
            List<TResponseEntity> entities = _mapper.Map<List<TResponseEntity>>(result.Item1);
            return QueryResponse<List<TResponseEntity>>.CreateInstance(entities, "", result.Item2);
        }


    }
}
