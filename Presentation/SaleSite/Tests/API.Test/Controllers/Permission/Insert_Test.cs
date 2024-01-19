using Domain.Entities;

namespace API.Test.Controllers
{
    public class Insert_Test : BaseController_Test, IDisposable
    {
        private readonly Mock<ICurrentUserSession> _currentUserService;
        private readonly PermissionController _controller;
        public Insert_Test()
        {
            _currentUserService = new Mock<ICurrentUserSession>();
            _controller = new PermissionController(_mediator.Object, _currentUserService.Object);
        }
        
        [Fact]
        public async Task InsertPermissionAPI_ReturnsSuccessStatusCode_ResultTest()
        {
            var permission = new Membership_Permission
            {
                AutoCode = 100,
                Title = "تنظیمات کاربر",
                CommandName = "Membership",
                FeatureType = (byte)Constants.FeatureType.Menu,
                IsActive = true,
                Name = "Membershp",
                ChildList = new List<Membership_Permission>(),
                FullKeyCode = "a100",
                Id = 1,
                LevelChar = 'a',

            };

            var command = new CreatePermissionCommand
            {
                CommandName = "Membership",
                FeatureType = (byte)Constants.FeatureType.Menu,
                IsActive = true,
                Name = "Membershp",
                Title = "تنظیمات کاربر"
            };

            _mediator
                .Setup(p => p.Send<CommandResponse<Membership_Permission>>(
                    It.IsAny<IRequest<CommandResponse<Membership_Permission>>>(), CancellationToken.None))
               .Returns(Task.FromResult(new CommandResponse<Membership_Permission>(true, permission)));


            var response = await _controller.Permissions(command, CancellationToken.None);

            Assert.True(response.IsSuccess);
            Assert.Equal(permission, response.Result);
        }
        public void Dispose()
        {

        }
    }
}
