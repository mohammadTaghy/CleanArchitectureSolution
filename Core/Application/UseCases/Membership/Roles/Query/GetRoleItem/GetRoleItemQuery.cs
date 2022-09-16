using Application.Common.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class GetRoleItemQuery : IRequest<QueryResponse<RolesDto>>
    {
        public int Id { get; set; }
    }
}
