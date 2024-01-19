namespace API.Test.Controllers
{
    public class GetCurrentUserPermissions_Test : BaseController_Test
    {
        private readonly Mock<ICurrentUserSession> _currentUserService;
        private readonly PermissionController _controller;

        public GetCurrentUserPermissions_Test()
        {
            _currentUserService = new Mock<ICurrentUserSession>();
            _controller = new PermissionController(_mediator.Object, _currentUserService.Object);
        }
        [Fact]
        public async Task GetCurrentUserPermissionsAPI_CannotGetData_Test()
        {
           
            _mediator.Setup(p => p.Send(It.IsAny<IRequest<QueryResponse<List<PermissionTreeDto>>>>(), CancellationToken.None))
                .Returns(Task.FromResult(QueryResponse<List<PermissionTreeDto>>.CreateInstance(
                    null, CommonMessage.Unauthorized, 0, false
                )));
            _currentUserService.Setup(p => p.UserId).Returns(1);
            var result = await _controller.CurrentUserPermissions(CancellationToken.None);
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Null(result.Result);
        }
        [Fact]
        public async Task GetCurrentUserPermissionsAPI_GetData_Test()
        {
            var permissionDto = new PermissionTreeDto
            {
                HasChild = false,
                Id = 1,
                Name = "Membership",
                Title = "مدیریت کاربران"
            };
            _mediator.Setup(p => p.Send(It.IsAny<IRequest<QueryResponse<List<PermissionTreeDto>>>>(), CancellationToken.None))
                .Returns(Task.FromResult(QueryResponse<List<PermissionTreeDto>>.CreateInstance(
                new List<PermissionTreeDto>
                    {
                        permissionDto
                    },"",1,true
                )));
            var result = await _controller.CurrentUserPermissions(CancellationToken.None);
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(new List<PermissionTreeDto>{ permissionDto }, result.Result);
        }
    }
}
