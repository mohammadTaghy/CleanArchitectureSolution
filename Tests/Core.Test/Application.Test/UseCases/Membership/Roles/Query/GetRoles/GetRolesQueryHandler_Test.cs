

namespace Application.Test.UseCases
{
    public class GetRolesQueryHandler_Test : UnitTestBaseQuery<Membership_Roles, IRolesRepoRead, IValidationRuleBase<Membership_Roles>>
    {
        private readonly GetRolesQueryHandler _handler;

        public GetRolesQueryHandler_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {

            _handler = new GetRolesQueryHandler(_repoMock.Object, _mapper, _cashManager.Object);
        }
        [Fact]
        public async Task GetRolesAsTreeQueryHandler_EmptyRoles_QueryTest()
        {
            int count = 0;
            _repoMock.Setup(p => p.ItemList(It.IsAny<ODataQueryOptions<Membership_Roles>>()))
                .Returns(Task.FromResult(new Tuple<List<Membership_Roles>, int>(new List<Membership_Roles>(), count)));

            var result = await _handler.Handle(getUserListQuery("https://localhost:8080?$top=10&$skip=0&$orderby=Id"), CancellationToken.None);

            Assert.Equal(new(), result.Result);
            Assert.Equal(CommonMessage.EmptyResponse, result.Message);
            Assert.Equal(count, result.TotalCount);

        }
        [Fact]
        public async Task GetRolesAsTreeQueryHandler_GetData_QueryTest()
        {
            int count = 1;
            Membership_Roles roles = new Membership_Roles
            {
                IsAdmin = false,
                Name = "test",
            };
            _repoMock.Setup(p => p.ItemList(It.IsAny<ODataQueryOptions<Membership_Roles>>()))
                .Returns(Task.FromResult(new Tuple<List<Membership_Roles>, int>(new List<Membership_Roles> { roles }, count)));

            QueryResponse<List<RolesDto>> result = await _handler.Handle(getUserListQuery("https://localhost:8080/?$top=10&$skip=0&$orderby=Id"), CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(roles.Name, result.Result.First().Name);
            Assert.Equal(count, result.TotalCount);

        }
        private RolesQuery getUserListQuery(string url)
        {
            var options = makeOdataQueryOption(url);
            RolesQuery _userListQueryRequest = new RolesQuery { ODataQuery = options };
            return _userListQueryRequest;
        }

    }
}
