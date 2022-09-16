using Application.Common.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class PermissionsAsTreeQuery : IRequest<QueryResponse<List<PermissionTreeDto>>>
    {
        public PermissionsAsTreeQuery() { }
        public PermissionsAsTreeQuery(int roleId)
        {
            RoleId = roleId;
        }

        public int RoleId { get; set; }
    }
}
