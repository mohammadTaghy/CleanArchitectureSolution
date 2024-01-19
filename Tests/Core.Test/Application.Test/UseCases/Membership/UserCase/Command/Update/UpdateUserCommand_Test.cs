using Application.UseCases.UserCase.Command.Update;
using Application.Validation;

namespace Application.Test.UseCases.UserCase.Command.Update
{
    public class UpdateUserCommand_Test:UnitTestBase<Membership_User, IUserRepo>
    {
        private readonly UpdateUserCommandHandler _handler;
        private readonly UpdateUserCommand _updateCommand;

        public UpdateUserCommand_Test(ITestOutputHelper testOutputHelper) :base(testOutputHelper)
        {
            _handler= new UpdateUserCommandHandler( _repoMock.Object, _mapper, _cacheManager.Object);
            _updateCommand = new UpdateUserCommand
            {
                Id=1,
                UserName = "MTY",
                MobileNumber = "0938776767",
                Email = "taghy@gmail.com",
                Password = "123456"
            };
        }
        [Fact]
        public void UpdateUserCommandHandler_IdIsZeroOrUserNameIsNull_ResultTest()
        {
            _updateCommand.UserName = null;
            _updateCommand.Id = 0;

            var result = Assert.ThrowsAsync<ArgumentNullException>(()=> _handler.Handle(_updateCommand, CancellationToken.None));

            _repoMock.Verify(p => p.Save(), Times.Never);
            Assert.Equal(
                string.Format(CommonMessage.NullException, $"{nameof(UpdateUserCommand.Id)} یا {nameof(UpdateUserCommand.UserName)}"), 
                result.Result.Message);
        }
        [Fact]
        public void UpdateUserCommandHandler_UserNotFound_RequestTest()
        {
            _repoMock.Setup(p => p.FindAsync(It.IsAny<int>(), It.IsAny<string>(),CancellationToken.None))
                .Returns(()=>Task.FromResult<Membership_User>(null));

            var result = Assert.ThrowsAsync<NotFoundException>(()=> _handler.Handle(_updateCommand, CancellationToken.None));

            _repoMock.Verify(p => p.Save(), Times.Never);
            Assert.Equal(String.Format(CommonMessage.NotFound, "کاربر"),result.Result.Message);

        }
        [Fact]
        public void UpdateUserCommandHandler_GiveCommand_ResultTest()
        {
            Membership_User _user = new Membership_User
            {
                Id = _updateCommand.Id,
                UserName = _updateCommand.UserName,
                MobileNumber = _updateCommand.MobileNumber,
                Email = _updateCommand.Email,
            };
            _repoMock.Setup(x => x.FindAsync(It.IsAny<int>(), It.IsAny<string>(), CancellationToken.None))
                .ReturnsAsync(_user);

            var result= _handler.Handle(_updateCommand, CancellationToken.None);

            _repoMock.Verify(p => p.Update(It.IsAny<UpdateUserCommand>()), Times.Once);
            Assert.True(result.IsCompleted);
        }

    }
}
