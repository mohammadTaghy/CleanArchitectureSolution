using Application.Common.Model;
using AutoMapper;
using Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class GetRolesQueryHandler : 
        BaseLoadListQueryHandler<GetRolesQuery, IRolesRepo,Membership_Roles, RolesDto>
    {
        public GetRolesQueryHandler(IRolesRepo repo, IMapper mapper) : base(repo, mapper)
        {
        }

    }
}
