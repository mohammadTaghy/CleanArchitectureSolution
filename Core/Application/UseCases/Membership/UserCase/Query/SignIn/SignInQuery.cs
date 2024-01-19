using MediatR;

namespace Application.UseCases.UserCase.Query.SignIn
{
    public class SignInQuery : IRequest<int>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
