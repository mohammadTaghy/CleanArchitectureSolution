using Application.UseCases.Membership.Permission.Command.Delete;
using System.Xml.Linq;

namespace Application.Test.UseCases.Membership.Permission.Command.Delete
{
    public class DeletePermissionCommand_Test : UnitTestBase<Membership_Permission, IPermissionRepo>
    {
        private readonly DeletePermissionCommand _deleteCommand;
        private readonly DeletePermissionCommandHandler _handler;
        private readonly Mock<IRolesRepo> rolesRepo;


        public DeletePermissionCommand_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {

            _deleteCommand = new DeletePermissionCommand
            {
                Id = 1,
            };
            rolesRepo = new Mock<IRolesRepo>();
            _handler = new DeletePermissionCommandHandler(_repoMock.Object, _mapper, rolesRepo.Object, _cacheManager.Object);
        }
        [Fact]
        public async Task DeletePermissionCommand_NullExcption_ResultTest()
        {
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(null, CancellationToken.None));

            Assert.Equal(string.Format(CommonMessage.NullException, "request"), exception.Message);
        }

        [Fact]
        public async Task DeletePermissionCommand_HasRefrenceInOtherTable_ResultTest()
        {

            rolesRepo.Setup(p => p.AnyEntity(It.IsAny<Expression<Func<Membership_Roles, bool>>>()))
                .Returns(Task.FromResult(true));

            var exception = await Assert.ThrowsAsync<DeleteFailureException>(() => _handler.Handle(_deleteCommand, CancellationToken.None));

            _repoMock.Verify(p => p.DeleteItem(It.IsAny<int>()), Times.Never);
        }
        [Fact]
        public async Task DeletePermissionCommand_FaildToDelete_ResultTest()
        {
            _repoMock.Setup(p => p.DeleteItem(It.IsAny<int>()))
                .Returns(Task.FromResult(false));

            var exception = await Assert.ThrowsAsync<DeleteFailureException>(() => _handler.Handle(_deleteCommand, CancellationToken.None));

            _repoMock.Verify(p => p.DeleteItem(It.IsAny<int>()), Times.Once);
            Assert.Equal(string.Format(CommonMessage.DeleteFailure, nameof(_deleteCommand.Id), _deleteCommand.Id), exception.Message);
        }
        [Fact]
        public async Task DeletePermissionCommand_SuccessResult_ResultTest()
        {
            rolesRepo.Setup(p => p.AnyEntity(It.IsAny<Expression<Func<Membership_Roles, bool>>>()))
                .Returns(Task.FromResult(false));
            _repoMock.Setup(p => p.DeleteItem(It.IsAny<int>()))
               .Returns(Task.FromResult(true));

            CommandResponse<bool> result = await _handler.Handle(_deleteCommand, CancellationToken.None);

            _repoMock.Verify(p => p.DeleteItem(It.IsAny<int>()), Times.Once);
            Assert.True(result.IsSuccess);
        }
    }
}
