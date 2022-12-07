using Application.Common.Exceptions;
using Application.Common.Model;
using AutoMapper;
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
        BaseLoadItemQueryHandler<GetRoleItemQuery, IRolesRepo, Membership_Roles, RolesDto>
    {
        public GetRoleItemQueryHandler(IRolesRepo repo, IMapper mapper) : base(repo, mapper)
        {
        }

    }
}
