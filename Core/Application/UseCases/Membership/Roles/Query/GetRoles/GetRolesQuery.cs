using Application.Common.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class GetRolesQuery : IRequest<QueryResponse<List<RolesDto>>>
    {
       

        public string SerchText { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
