using Application.Common.Model;
using AutoMapper;
using Common;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public abstract class BaseCommandHandler<TRequest, TResponse, TRepo> : IRequestHandler<TRequest, TResponse>
        where TRequest : class, IRequest<TResponse>, new()
    {
        protected readonly TRepo _repo;
        protected readonly IMapper _mapper;
        protected readonly ICacheManager _cacheManager;
        public BaseCommandHandler(TRepo repo, IMapper mapper, ICacheManager cacheManager)
        {
            _repo = repo;
            _mapper = mapper;   
            _cacheManager = cacheManager;
        }
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
