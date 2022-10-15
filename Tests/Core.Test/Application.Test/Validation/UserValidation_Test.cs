using Application.Validation;
using Common;
using Domain;
using Domain.Entities;
using FluentValidation.TestHelper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Application.Test.Validation
{
    public class UserValidation_Test: UnitTestBase<Membership_User,IUserRepo,IUserValidation>
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
