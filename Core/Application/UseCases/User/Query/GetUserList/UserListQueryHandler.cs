using Application.Mappings;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.User.Query.GetUserList
{
    public class UserListQueryHandler : IRequestHandler<UserListQueryRequest, List<UserListDto>>
    {
        private readonly IUserRepoRead _userRepoRead;
        public readonly IMapper _mappingProfile;


        public UserListQueryHandler(IUserRepoRead userRepoRead, IMapper mappingProfile)
        {
            _userRepoRead = userRepoRead;
            _mappingProfile = mappingProfile;
        }


        public async Task<List<UserListDto>> Handle(UserListQueryRequest request, CancellationToken cancellationToken)
        {
            var query=_userRepoRead.Queryable;
            if(!string.IsNullOrEmpty(request.UserName))
                query = query.Where(x => x.UserName == request.UserName);   
            if(!string.IsNullOrEmpty(request.FullName))
                query=query.Where(p=>(p.FirstName+ " "+p.LastName).Contains(request.FullName));
            if (!string.IsNullOrEmpty(request.NationalCode))
                query = query.Where(p => p.NationalCode.Contains(request.NationalCode));
            if (!string.IsNullOrEmpty(request.MobileNumber))
                query = query.Where(p => p.MobileNumber.Contains(request.MobileNumber));
            return  query.ProjectTo<UserListDto>(_mappingProfile.ConfigurationProvider).ToList();
            
        }
    }
}
