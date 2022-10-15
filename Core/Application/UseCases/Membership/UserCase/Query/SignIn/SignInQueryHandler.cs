using Application.Common.Exceptions;
using Application.UseCases.UserProfileCase.Query.GetUserItem;
using AutoMapper;
using Common;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UserCase.Query.SignIn
{
    public class SignInQueryHandler : IRequestHandler<SignInQuery, int>
    {
        private readonly IUserRepo _userRepoRead;
        public readonly IMapper _mappingProfile;


        public SignInQueryHandler(IUserRepo userRepoRead, IMapper mappingProfile)
        {
            _userRepoRead = userRepoRead;
            _mappingProfile = mappingProfile;
        }

        public async Task<int> Handle(SignInQuery request, CancellationToken cancellationToken)
        {
            Membership_User user =await _userRepoRead.FindAsync(null, request.UserName, cancellationToken);
            if (user!=null && user.PasswordHash == UtilizeFunction.CreateMd5(request.Password))
                return user.Id;
            else
                throw new NotFoundException(request.UserName,"");
        }
    }
}
