using Application.UseCases.Membership.UserCase.Query.GetUserItem;

namespace Application.Test.UseCases.Membership.UserCase.Query.GetUser
{
    public class GetUser_Test : UnitTestBaseQuery<Membership_User, IUserRepoRead, IValidationRuleBase<Membership_User>>
    {
        private readonly GetUserItemQueryHandler _handler;
        private readonly UserItemQuery _userListQueryRequest;

        public GetUser_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _handler = new GetUserItemQueryHandler(_repoMock.Object, _mapper, _cashManager.Object);
            _userListQueryRequest = new UserItemQuery
            {
                Id = 1,
            };
        }
        [Fact]
        public async Task GetUserItem_NullRequest_QueryTest()
        {
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(null, CancellationToken.None));

            Assert.Equal(string.Format(CommonMessage.NullException, "request"), exception.Message);
        }
        [Fact]
        public async Task GetUserItem_Succes_QueryTest()
        {
            _repoMock
                .Setup(p => p.FindOne(It.IsAny<int>()))
                .Returns(Task.FromResult( new Membership_User
                {
                    Id = 1,
                    UserName = "Admin",
                    IsUserConfirm = true
                }
                ));

            var result = await _handler.Handle(_userListQueryRequest, CancellationToken.None);

            _repoMock.Verify(p => p.FindOne(It.IsAny<int>()), Times.Once);
            Assert.True(result.IsSuccess);
        }
    }
}
