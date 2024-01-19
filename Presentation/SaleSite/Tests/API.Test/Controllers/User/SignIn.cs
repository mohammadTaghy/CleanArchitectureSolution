using Application.UseCases.Membership.UserCase;
using Application.UseCases.UserCase.Query.SignIn;

namespace API.Test.Controllers.User
{
    public class SignIn : BaseController_Test
    {
        private readonly UserController _controller;
        public SignIn()
        {
            _controller = new UserController(_mediator.Object,_currentUserService.Object,_configuration.Object);
        }
        [Fact]
        public async Task SignIn_ReturnSuccess_ResultTest()
        {
            var result = new CommandResponse<UserDto>(true,
                  new UserDto
                  {
                      
                  });
            _mediator.Setup(p=>p.Send(It.IsAny<IRequest<int>>(),CancellationToken.None)).
                Returns(Task.FromResult(1));
            var query=new SignInQuery()
            {
                Password="1234",
                UserName="test",
            };
            var response = await _controller.SignIn(query, CancellationToken.None);
            Assert.NotNull(response);
            Assert.True(response.IsSuccess);
            Assert.Equal(result.Result, response.Result);
        }
    }
}
