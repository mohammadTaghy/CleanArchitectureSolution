using API.Controllers;
using Application.Common.Model;
using Application.UseCases.UserCase.Query.SignIn;
using Application.UseCases.UserProfileCase.Query.GetUserItem;
using Common.JWT;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var result = new CommandResponse<UserItemDto>(true,
                  new UserItemDto
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
