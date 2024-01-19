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
        BaseLoadListQueryHandler<RolesQuery, IRolesRepoRead,Membership_Roles, RolesDto>
    {
        public GetRolesQueryHandler(IRolesRepoRead repo, IMapper mapper, ICacheManager cacheManager) : base(repo, mapper, cacheManager)
        {
        }

    }
}
