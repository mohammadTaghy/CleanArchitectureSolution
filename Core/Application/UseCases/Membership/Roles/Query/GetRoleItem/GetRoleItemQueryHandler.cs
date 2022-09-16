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
    public class GetRoleItemQueryHandler : BaseCommandHandler<GetRoleItemQuery, QueryResponse<RolesDto>, IRolesRepo>
    {
        public GetRoleItemQueryHandler(IRolesRepo repo, IMapper mapper) : base(repo, mapper)
        {
        }

        public async override Task<QueryResponse<RolesDto>> Handle(GetRoleItemQuery request, CancellationToken cancellationToken)
        {
            Roles role =await _repo.FindAsync(request.Id);
            if (role is null)
                throw new NotFoundException($"نقش با کد رایانه {request.Id}",nameof(Roles));

            return QueryResponse<RolesDto>.CreateInstance(_mapper.Map<RolesDto>(role), "", 1);
        }
    }
}
