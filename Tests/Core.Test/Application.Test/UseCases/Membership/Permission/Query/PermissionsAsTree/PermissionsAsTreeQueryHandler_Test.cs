namespace Application.Test.UseCases.PermissionUseCase.Query.GetPermissionAsTree
{
    public class PermissionsAsTreeQueryHandler_Test : UnitTestBaseQuery<Membership_Permission, IPermissionRepoRead, IValidationRuleBase<Membership_Permission>>
    {
        private readonly PermissionsAsTreeQuery _query;
        private readonly PermissionsAsTreeQueryHandler _handler;

        public PermissionsAsTreeQueryHandler_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _query = new PermissionsAsTreeQuery { RoleId=1};
            Mock<ICacheManager> cacheManager= new Mock<ICacheManager>();
            _handler = new PermissionsAsTreeQueryHandler(_repoMock.Object, _mapper, cacheManager.Object);
        }
        [Fact]
        public async Task PermissionsAsTreeQueryHandler_NullRequest_QueryTest()
        {
            
            var exception = await Assert.ThrowsAnyAsync< BadRequestException >(()=> _handler.Handle(null, CancellationToken.None));

            Assert.Equal(string.Format(CommonMessage.NullException, "Permission"), exception.Message);

        }
        [Fact]
        public async Task PermissionsAsTreeQueryHandler_EmptyPermission_QueryTest()
        {
            _repoMock.Setup(p => p.GetPermissions(It.IsAny<int>()))
                .Returns(Task.FromResult(new List<PermissionTreeDto>()));

            var result = await _handler.Handle(_query,CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(new (), result.Result);
            Assert.Equal(CommonMessage.EmptyResponse, result.Message);
            Assert.Equal(0,result.TotalCount);

        }
        [Fact]
        public async Task PermissionsAsTreeQueryHandler_GetData_QueryTest()
        {
            var permissionDto = new PermissionTreeDto
            {
                HasChild = false,
                Id = 1,
                Name = "Membership",
                Title = "مدیریت کاربران"
            };
            _repoMock.Setup(p => p.GetPermissions(It.IsAny<int>()))
                .Returns(Task.FromResult(new List<PermissionTreeDto> { permissionDto }));

            var result = await _handler.Handle(_query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(new List<PermissionTreeDto>() { permissionDto }, result.Result);
            Assert.Equal(1, result.TotalCount);

        }
    }
}
