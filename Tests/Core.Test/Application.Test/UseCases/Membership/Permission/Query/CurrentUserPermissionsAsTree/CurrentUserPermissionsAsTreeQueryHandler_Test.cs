namespace Application.Test.UseCases.PermissionUseCase.Query.GetPermissionAsTree
{
    public class CurrentUserPermissionsAsTreeQueryHandler_Test : UnitTestBaseQuery<Membership_Permission, IPermissionRepoRead, IValidationRuleBase<Membership_Permission>>
    {
        private readonly CurrentUserPermissionsAsTreeQuery _query;
        private readonly CurrentUserPermissionsAsTreeQueryHandler _handler;

        public CurrentUserPermissionsAsTreeQueryHandler_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _query = new CurrentUserPermissionsAsTreeQuery();
            _handler = new CurrentUserPermissionsAsTreeQueryHandler(_repoMock.Object, _mapper, _cashManager.Object);
        }
        [Fact]
        public void CurrentUserPermissionsAsTreeQueryHandler_EmptyPermission_QueryTest()
        {
            _repoMock.Setup(p => p.GetCurrentRolePermissions(It.IsAny<int>()))
                .Returns(Task.FromResult(new List<PermissionTreeDto>()));

            var result = _handler.Handle(_query,CancellationToken.None);

            Assert.True(result.IsCompletedSuccessfully);
            Assert.Equal(new (), result.Result.Result);
            Assert.Equal(CommonMessage.Unauthorized, result.Result.Message);
            Assert.Equal(0,result.Result.TotalCount);

        }
        [Fact]
        public void CurrentUserPermissionsAsTreeQueryHandler_GetData_QueryTest()
        {
            var permissionDto = new PermissionTreeDto
            {
                HasChild = false,
                Id = 1,
                Name = "Membership",
                Title = "مدیریت کاربران"
            };
            _repoMock.Setup(p => p.GetCurrentRolePermissions(It.IsAny<int>()))
                .Returns(Task.FromResult(new List<PermissionTreeDto> { permissionDto }));

            var result = _handler.Handle(_query, CancellationToken.None);

            Assert.True(result.IsCompletedSuccessfully);
            Assert.Equal(new List<PermissionTreeDto>() { permissionDto }, result.Result.Result);
            Assert.Equal(1, result.Result.TotalCount);

        }
    }
}
