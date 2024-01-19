using Application.UseCases.Membership.Permission.Command.Create;

namespace Application.Test.UseCases
{
    public class CreatePermissionCommand_Test : UnitTestBase<Membership_Permission,IPermissionRepo>
    {
        private readonly CreatePermissionCommand _createCommand;
        private readonly CreatePermissionCommandHandler _handler;


        public CreatePermissionCommand_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            
            _createCommand = new CreatePermissionCommand
            {
                CommandName = "CommandTest",
                FeatureType =(byte) Constants.FeatureType.Command,
                IsActive = true,
                Name = "Test",
                Title="test"
            };

            _handler = new CreatePermissionCommandHandler(_repoMock.Object, _mapper,_cacheManager.Object);
        }
        [Fact]
        public async Task CreatePermissionCommand_NullExcption_ResultTest()
        {
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(null, CancellationToken.None));

            Assert.Equal(string.Format(CommonMessage.NullException, "Permission"), exception.Message);
        }
        [Fact]
        public async Task CreatePermissionCommand_ValidationExcption_ResultTest()
        {
            var validation = new CreatePermissionCommandValidator();
            CreatePermissionCommand command = new CreatePermissionCommand
            {
                CommandName = "CommandTest",
                Name = "T",
                Title = "test"
            };
            var failur = await validation.ValidateAsync(command);

            Assert.False(failur.IsValid);
            Assert.True(failur.Errors.Count>0);
        }
        [Fact]
        public async Task CreatePermissionCommand_SuccessResult_ResultTest()
        {
            CommandResponse<Membership_Permission> result = await _handler.Handle(_createCommand, CancellationToken.None);

            _repoMock.Verify(p => p.Insert(It.IsAny<Membership_Permission>()), Times.Once);
            Assert.True(result.IsSuccess);
        }
        
    }
}
