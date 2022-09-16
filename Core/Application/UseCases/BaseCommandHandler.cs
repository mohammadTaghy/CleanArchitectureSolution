using Application.Common.Model;
using AutoMapper;
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
        public BaseCommandHandler(TRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;   
        }
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
