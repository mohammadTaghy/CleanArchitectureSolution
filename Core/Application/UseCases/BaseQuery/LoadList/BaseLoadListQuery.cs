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
    public class BaseLoadListQuery<TResponse> : IRequest<TResponse>
        where TResponse : class,new()
    {
        public string Filter { get; set; }
        public string Columns { get; set; }
        public int Top { get; set; }
        public int Skip { get; set; }
        public string Orderby { get; set; }
        
    }
}
