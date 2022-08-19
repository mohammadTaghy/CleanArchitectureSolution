using Application.UseCases.UserProfileCase.Query.GetUserItem;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UserCase.Query.SignIn
{
    public class SignInQuery : IRequest<int>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
