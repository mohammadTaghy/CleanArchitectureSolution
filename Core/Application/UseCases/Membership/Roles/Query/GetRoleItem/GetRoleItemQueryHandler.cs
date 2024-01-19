using Application.Common.Exceptions;
using Application.Common.Model;
using AutoMapper;
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
    public class GetRoleItemQueryHandler : 
        BaseLoadItemQueryHandler<GetRoleItemQuery, IRolesRepoRead, Membership_Roles, RolesDto>
    {
        public GetRoleItemQueryHandler(IRolesRepoRead repo, IMapper mapper, ICacheManager cacheManager) : base(repo, mapper, cacheManager)
        {
        }

    }
}
