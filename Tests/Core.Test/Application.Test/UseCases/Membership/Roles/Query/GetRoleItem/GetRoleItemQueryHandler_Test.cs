using Application.Common.Exceptions;

namespace Application.Test.UseCases
{
    public class GetRoleItemQueryHandler_Test : UnitTestBaseQuery<Membership_Roles, IRolesRepoRead, IValidationRuleBase<Membership_Roles>>
    {
        private readonly GetRoleItemQuery _query;
        private readonly GetRoleItemQueryHandler _handler;

        public GetRoleItemQueryHandler_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _query = new GetRoleItemQuery() { Id = 1 };
            Mock<ICacheManager> cacheManager = new Mock<ICacheManager>();
            _handler = new GetRoleItemQueryHandler(_repoMock.Object, _mapper, cacheManager.Object);
        }
        [Fact]
        public async Task GetRoleItemQueryHandler_NotFind_QueryTest()
        {
            Membership_Roles roles = null;
            _repoMock.Setup(p => p.FindOne(It.IsAny<int>()))
                .Returns(Task.FromResult(roles));

            var exception = await Assert.ThrowsAnyAsync<NotFoundException>(()=>_handler.Handle(_query, CancellationToken.None));

            Assert.Equal(String.Format(CommonMessage.NotFound, _query.Id), 
                exception.Message);

        }
        [Fact]
        public async Task GetRolesAsTreeQueryHandler_GetData_QueryTest()
        {
            var roles = new Membership_Roles
            {
                IsAdmin = false,
                Name = "test",
            };
            var rolesDto = new RolesDto
            {
                IsAdmin = false,
                Name = "test",
            };
            _repoMock.Setup(p => p.FindOne(It.IsAny<int>()))
                .Returns(Task.FromResult(roles));

            var result = await _handler.Handle(_query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal( rolesDto.Name, result.Result.Name);

        }
    }
}
