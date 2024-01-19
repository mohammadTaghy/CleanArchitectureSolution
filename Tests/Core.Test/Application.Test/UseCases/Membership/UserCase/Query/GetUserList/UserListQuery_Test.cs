using Application.UseCases.Membership.UserCase.Query.GetUserList;

namespace Application.Test.UseCases.Membership.UserCase.Query.GetUserList
{
    public class UserListQuery_Test : UnitTestBaseQuery<Membership_User, IUserRepoRead, IValidationRuleBase<Membership_User>>
    {
        private readonly UserListQueryHandler _handler;


        public UserListQuery_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _handler = new UserListQueryHandler(_repoMock.Object, _mapper, _cashManager.Object);

        }
        [Fact]
        public void UserListQueryHandler_NullRequest_QueryTest()
        {
            var exception = Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(null, CancellationToken.None));
            Assert.Equal(string.Format(CommonMessage.NullException, "request"), exception.Result.Message);
        }
        [Fact]
        public void UserListQueryHandler_NotErrorWhenTopLessThanZero_QueryTest()
        {

            var result = _handler.Handle(getUserListQuery("https://localhost:8080/?$top=10&$skip=0&$orderby=Id"), CancellationToken.None);
            int total = 0;
            _repoMock.Verify(p => p.ItemList(It.IsAny<ODataQueryOptions<Membership_User>>()), Times.Once);
            Assert.True(result.IsCompleted);
        }
        [Fact]
        public void UserListQueryHandler_Succes_QueryTest()
        {
            //_userProfileRepo.Setup(p=>p.ItemList(It.IsAny<UserListQueryHandler>()))
            //    .Return()
            var result = _handler.Handle(getUserListQuery("https://localhost:8080/?$top=10&$skip=0&$orderby=Id"), CancellationToken.None);
            int total = 0;
            _repoMock.Verify(p => p.ItemList(It.IsAny<ODataQueryOptions<Membership_User>>()), Times.Once);
            Assert.True(result.IsCompleted);
        }

        private UserListQuery getUserListQuery(string url)
        {
            var options = makeOdataQueryOption(url);
            UserListQuery _userListQueryRequest = new UserListQuery { ODataQuery = options };
            return _userListQueryRequest;
        }
    }
}
