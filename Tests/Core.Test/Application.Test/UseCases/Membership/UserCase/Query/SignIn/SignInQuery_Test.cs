using Application.UseCases.UserCase.Query.SignIn;
using Application.Validation;

namespace Application.Test.UseCases.UserCase.Query.SignIn
{
    public class SignInQuery_Test : UnitTestBase<Membership_User, IUserRepo>
    {
        private readonly SignInQueryHandler _handler;
        private readonly SignInQuery _signInQuery;
        public SignInQuery_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _handler = new SignInQueryHandler(_repoMock.Object, _mapper);
            _signInQuery = new SignInQuery
            {
                UserName = "test",
                Password = "1234",
            };
        }
        [Fact]
        public void SignIn_CorrectInfo_QueryTest()
        {
            _repoMock.Setup(p => p.FindAsync(It.IsAny<int?>(), It.IsAny<string>(), CancellationToken.None))
                .Returns(Task.FromResult(new Membership_User()
                {
                    UserName = _signInQuery.UserName,
                    PasswordHash = UtilizeFunction.CreateMd5("1234"),
                    Id = 1
                }));
            var result = _handler.Handle(_signInQuery, CancellationToken.None);
            Assert.NotNull(result);
            Assert.True(result.IsCompletedSuccessfully);
            Assert.Equal(1, result.Result);
        }
        [Fact]
        public void SignIn_InCorrectInfo_QueryTest()
        {
            _repoMock.Setup(p => p.FindAsync(It.IsAny<int?>(), It.IsAny<string>(), CancellationToken.None))
                .Returns(Task.FromResult(new Membership_User()
                {
                    UserName = _signInQuery.UserName,
                    PasswordHash = UtilizeFunction.CreateMd5("12345"),
                    Id = 1
                }));
            var result = _handler.Handle(_signInQuery, CancellationToken.None);
            Assert.True(result.IsFaulted);
            Assert.Equal(new NotFoundException(_signInQuery.UserName,_signInQuery.Password).Message, result.Exception.GetBaseException().Message);
        }
    }
}
