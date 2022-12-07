using Application.Common.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class PermissionsAsTreeQuery : BaseLoadListQuery<QueryResponse<List<PermissionTreeDto>>>
    {
        public int RoleId { get; set; }
    }
}
