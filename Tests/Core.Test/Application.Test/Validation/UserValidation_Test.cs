using Application.Validation;
using Common;
using Domain;
using FluentValidation.TestHelper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Application.Test.Validation
{
    public class UserValidation_Test: UnitTestBase
    {
        private readonly Mock<IUserRepoRead> _userRepo;
        private readonly IUser _user;
        private readonly UserValidation validation;

        public UserValidation_Test(ITestOutputHelper testOutputHelper):base(testOutputHelper)
        {
            _userRepo = new Mock<IUserRepoRead>();
            _user = Mock.Of<IUser>();
            validation = new UserValidation(_userRepo.Object);
        }
        [Fact]
        public void ValidationExceptionIfIsNull()
        {

            _userRepo.Setup(x => x.CheckUniqUserName(It.IsAny<string>(), It.IsAny<int>())).
                Returns(true);
            _user.UserName = string.Empty;
            var validate = validation.TestValidate(_user);
            Assert.True(validate.IsValid);
        }
        public enum UserValidationValid
        {
            IsValid,
            IsNotValid
        }
    }

}
