using Application.UseCases.UserCase.Command.Create;
using Application.Validation;

namespace Application.Test.UseCases.UserCase.Command.Create
{
    public class CreateUserCommand_Test : UnitTestBase<Membership_User,IUserRepo>
    {
       
        private readonly CreateUserCommand _createCommand;
        private readonly List<Membership_User> _users;
        private readonly CreateUserCommandHandler _handler;

        public CreateUserCommand_Test(ITestOutputHelper testOutputHelper):base(testOutputHelper)
        {
           
            _createCommand = new CreateUserCommand
            {
                UserName = "MTY",
                MobileNumber = "0938776767",
                Email="taghy@gmail.com",
                Password="123456"
            };
            _users = new List<Membership_User>() { new Membership_User {
                Id=1,
                Email=_createCommand.Email,
                UserName=_createCommand.UserName,
                MobileNumber=_createCommand.MobileNumber,
            } };
            
            _handler = new CreateUserCommandHandler( _repoMock.Object, _mapper, _cacheManager.Object);
        }
        [Fact]
        public async Task CreateUserCommandHandler_NullExcption_ResultTest()
        {
            var exception = await Assert.ThrowsAsync<BadRequestException>(()=>_handler.Handle(null, CancellationToken.None));

            Assert.Equal(string.Format(CommonMessage.NullException, "request"),exception.Message);
        }
        [Fact]
        public async Task CreateUserCommandHandler_CheckExistUser_ResultTest()
        {
            bool existUser=false;
            _repoMock.Setup(p => p.AnyEntity(It.IsAny<Expression<Func<Membership_User, bool>>>()))
                .Returns(Task.FromResult<bool>(true));

            var exception = await Assert.ThrowsAsync<ValidationException>(()=> _handler.Handle(_createCommand, CancellationToken.None));

            _repoMock.Verify(p => p.Insert(It.IsAny<Membership_User>()), Times.Never);
            Assert.Equal(String.Format(CommonMessage.IsDuplicateUserName, _createCommand.UserName), exception.Failures.First().Value.First());
        }
        [Fact]
        public void CreateUserCommandHandler_SuccessResult_ResultTest()
        {
            Task<CommandResponse<int>> result = null;
            _repoMock.Setup(p => p.AnyEntity(It.IsAny<Expression<Func<Membership_User, bool>>>()))
                .Returns(Task.FromResult<bool>(false));

            result = _handler.Handle(_createCommand, CancellationToken.None);

            _repoMock.Verify(p => p.Insert(It.IsAny<CreateUserCommand>()), Times.Once);
            Assert.True(result.Result.IsSuccess);
        }

    }
}
