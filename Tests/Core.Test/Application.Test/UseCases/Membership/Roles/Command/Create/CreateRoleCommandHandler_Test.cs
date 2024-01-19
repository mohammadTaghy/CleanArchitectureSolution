
namespace Application.Test.UseCases
{
    public class CreateRolesCommand_Test : UnitTestBase<Membership_Roles, IRolesRepo>
    {
        private readonly UpdateRoleCommand _createCommand;
        private readonly UpdateRoleCommandHandler _handler;

        public CreateRolesCommand_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {

            _createCommand = new UpdateRoleCommand
            {
                Name = "TestRole",
                IsAdmin = true
            };


            _handler = new UpdateRoleCommandHandler(_repoMock.Object, _mapper, _cacheManager.Object);
        }
        [Fact]
        public void CreateRoleCommand_NullExcption_ResultTest()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));
            Assert.Equal(string.Format(CommonMessage.NullException, "Roles"), exception.Result.Message);
        }
        [Fact]
        public async Task CreateRoleCommand_NotFoundException_ResultTest()
        {
            _repoMock.Setup(p => p.AnyEntity(It.IsAny<Expression<Func<Membership_Roles, bool>>>()))
                .Returns(Task.FromResult(false));

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(_createCommand, CancellationToken.None));

            Assert.Equal(String.Format(CommonMessage.NotFound, "Role"), exception.Message);
        }
        [Fact]
        public void CreateRoleCommand_SuccessResult_ResultTest()
        {
            Task<CommandResponse<Membership_Roles>> result = null;
            _repoMock.Setup(p => p.AnyEntity(It.IsAny<Expression<Func<Membership_Roles, bool>>>()))
                .Returns(Task.FromResult(true));

            result = _handler.Handle(_createCommand, CancellationToken.None);
            _repoMock.Verify(p => p.Update(It.IsAny<UpdateRoleCommand>()), Times.Once);

            Assert.True(result.Result.IsSuccess);
        }
       
    }
}
