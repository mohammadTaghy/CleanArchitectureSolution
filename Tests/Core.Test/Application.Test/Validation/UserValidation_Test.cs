using Application.Validation;

namespace Application.Test.Validation
{
    public class UserValidation_Test: UnitTestBase<Membership_User, IUserRepo>
    {

        public UserValidation_Test(ITestOutputHelper testOutputHelper):base(testOutputHelper)
        {
        }
        //[Fact]
        //public void ValidationExceptionIfIsNull()
        //{
        //    _repoMock.Setup(p => p.AnyEntity(It.IsAny<Expression<Func<User, bool>>>()))
        //        .Returns(Task.FromResult(true));

        //    User user = new User();

        //    UserValidation validations = new UserValidation(_repoMock.Object);
        //    var validate = validations.TestValidate(user);
        //    Assert.True(validate.IsValid);
        //}
        public enum UserValidationValid
        {
            IsValid,
            IsNotValid
        }
    }

}
