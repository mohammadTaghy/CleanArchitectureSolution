using Application.Common.Model;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class RolesQuery : BaseLoadListQuery<QueryResponse<List<RolesDto>>,Membership_Roles>
    {
       
    }
}
