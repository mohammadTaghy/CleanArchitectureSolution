using Application.Common.Model;
using AutoMapper;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class GetRolesQueryHandler : BaseCommandHandler<GetRolesQuery, QueryResponse<List<RolesDto>>, IRolesRepo>
    {
        public GetRolesQueryHandler(IRolesRepo repo, IMapper mapper) : base(repo, mapper)
        {
        }

        public override async Task<QueryResponse<List<RolesDto>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            int count;
            List<RolesDto> roles = await _repo.ItemsAsList(request,out count);
            QueryResponse<List<RolesDto>> response = null;
            if (count==0)
                response = QueryResponse<List<RolesDto>>.CreateInstance(new List<RolesDto>(), 
                    CommonMessage.EmptyResponse,count,true);
            else
                response= QueryResponse<List<RolesDto>>.CreateInstance(roles,"",count,true);
            return response;
        }
    }
}
