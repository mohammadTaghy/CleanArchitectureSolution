using Application.Common.Model;
using Application.Mappings;
using Common;
using Domain;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.OData.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class BaseLoadListQuery<TResponse, TEntity> : IRequest<TResponse>
        where TResponse : class, new()
        where TEntity : IEntity
    {
        public ODataQueryOptions<TEntity> ODataQuery { get; set; }

    }
}
