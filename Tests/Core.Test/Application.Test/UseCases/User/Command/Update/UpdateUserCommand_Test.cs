using Application.Common;
using Application.IUtils;
using Application.UseCases.User.Command.Update;
using Application.Validation;
using Common;
using Domain;
using Infrastructure.Utils;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Application.Test.UseCases.User.Command.Update
{
    public class UpdateUserCommand_Test:UnitTestBase
    {
        private readonly Mock<IMessages> _messages;
        private readonly Mock<IUserValidation> _userValidation;
        private readonly Mock<IUserRepo> _userRepo;
        private readonly IUser _user;

        public UpdateUserCommand_Test(ITestOutputHelper testOutputHelper) :base(testOutputHelper)
        {
            _messages = new Mock<IMessages>();
            _userValidation = new Mock<IUserValidation>();
            _user = Mock.Of<IUser>();
            _userRepo = new Mock<IUserRepo>();
        }
        [Fact]
        public void UpdateUserCommandHandler_UserIdOrUserNameIsNull_ResultTest()
        {
            _userRepo.Setup(x => x.FindAsync(It.IsAny<int>())).ReturnsAsync(()=>null);
            var handler = new UpdateUserCommand.UpdateUserCommandHandler(_userValidation.Object, _userRepo.Object);
            var updateCommand = new UpdateUserCommand("MTY", "Mohammad", "Yami", 1);
            var result = handler.Handle(updateCommand, CancellationToken.None);
            Assert.False(result.IsSuccess);
        }
        [Fact]
        public void UpdateUserCommandHandler_GiveCommand_ResultTest()
        {
            _userRepo.Setup(x => x.FindAsync(It.IsAny<int>())).ReturnsAsync(_user);
            _userRepo.Setup(x => x.Save());
            var handler= new UpdateUserCommand.UpdateUserCommandHandler(_userValidation.Object, _userRepo.Object);
            var updateCommand = new UpdateUserCommand("MTY", "Mohammad", "Yami", 1);
            var result= handler.Handle( updateCommand, CancellationToken.None);
            Assert.True(result.IsSuccess);
            Assert.Equal(updateCommand.FirstName, _user.FirstName);
            Assert.Equal(updateCommand.LastName, _user.LastName);
        }

    }
}
