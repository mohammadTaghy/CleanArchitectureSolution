using Application.Common.Model;
using Application.Mappings;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class CurrentUserPermissionsAsTreeQuery : BaseLoadListQuery<QueryResponse<List<PermissionTreeDto>>,Membership_Permission>
    {
        public int UserId { get; set; }
    }
}
