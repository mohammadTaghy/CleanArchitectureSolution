using Application.UseCases.Membership.Permission.Command.Create;
using Application.UseCases.Membership.Permission.Command.Update;

namespace Application.Test.UseCases.Membership.Permission.Command.Update
{
    public class UpdatePermissionCommand_Test : UnitTestBase<Membership_Permission, IPermissionRepo>
    {
        private readonly UpdatePermissionCommand _createCommand;
        private readonly UpdatePermissionCommandHandler _handler;


        public UpdatePermissionCommand_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {

            _createCommand = new UpdatePermissionCommand
            {
                CommandName = "CommandTest",
                FeatureType = (byte)Constants.FeatureType.Command,
                IsActive = true,
                Name = "Test",
                Title = "test"
            };

            _handler = new UpdatePermissionCommandHandler(_repoMock.Object, _mapper, _cacheManager.Object);
        }

        [Fact]
        public async Task UpdatePermissionCommand_NullExcption_ResultTest()
        {
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(null, CancellationToken.None));

            _repoMock.Verify(p => p.Update(It.IsAny<Membership_Permission>()), Times.Never);
            Assert.Equal(string.Format(CommonMessage.NullException, "Permission"), exception.Message);
        }
        [Fact]
        public async Task UpdatePermissionCommand_ValidationExcption_ResultTest()
        {
            var validation = new UpdatePermissionCommandValidator();
            UpdatePermissionCommand command = new UpdatePermissionCommand
            {
                CommandName = "CommandTest",
                Name = "T",
                Title = "test"
            };

            var failur = await validation.ValidateAsync(command);

            _repoMock.Verify(p => p.Update(It.IsAny<Membership_Permission>()), Times.Never);
            Assert.False(failur.IsValid);
            Assert.True(failur.Errors.Count > 0);
        }
        [Fact]
        public async Task UpdatePermissionCommand_SuccessResult_ResultTest()
        {
            CommandResponse<Membership_Permission> result = await _handler.Handle(_createCommand, CancellationToken.None);

            _repoMock.Verify(p => p.Update(It.IsAny<Membership_Permission>()), Times.Once);

            Assert.True(result.IsSuccess);
        }
    }
}
