using Application.Common.Model;
using Application.Mappings;
using Application.UseCases.UserProfileCase.Command.Create;
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
    public class BaseLoadItemQuery<TResponse> : IRequest<TResponse>
        where TResponse : class,new()
    {
        public int Id { get; set; }
    }
}
