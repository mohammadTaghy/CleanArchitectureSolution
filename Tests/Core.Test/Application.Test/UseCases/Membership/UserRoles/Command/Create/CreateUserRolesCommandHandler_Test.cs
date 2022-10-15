using Application.Common.Interfaces;
using Application.UseCases;
using Common;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit;

namespace Application.Test.UseCases
{
    public class CreateUserRolesCommandHandler_Test : UnitTestBase<Membership_UserRoles, IUserRolesRepo, IValidationRuleBase<Membership_UserRoles>>, IDisposable
    {
        private readonly CreateUserRolesCommand _createCommand;
        private readonly CreateUserRolesCommandHandler _handler;

        public CreateUserRolesCommandHandler_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {

            _createCommand = new CreateUserRolesCommand
            {
                UserId = 1,
                RoleIds = new List<int> { 1, 2 },
            };


            _handler = new CreateUserRolesCommandHandler(_repoMock.Object, _mapper.Object);
        }
        [Fact]
        public void CreateUserRolesCommand_NullExcption_ResultTest()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));
            Assert.Equal(string.Format(CommonMessage.NullException, "UserRoles"), exception.Result.Message);
        }
        [Fact]
        public void CreateUserRolesCommand_HasError_ResultTest()
        {

            _repoMock.Setup(p => p.Insert(It.IsAny<CreateUserRolesCommand>()))
                .Returns(Task.FromResult(false));
            var exception = Assert.ThrowsAsync<Exception>(() => _handler.Handle(_createCommand, CancellationToken.None));
            _repoMock.Verify(p => p.Insert(It.IsAny<CreateUserRolesCommand>()), Times.Once);
            Assert.Equal(CommonMessage.Error, exception.Result.Message);
        }
        [Fact]
        public async void CreateUserRolesCommand_SuccessResult_ResultTest()
        {

            _repoMock.Setup(p => p.Insert(It.IsAny<CreateUserRolesCommand>()))
                .Returns(Task.FromResult(true));
            var result = await _handler.Handle(_createCommand, CancellationToken.None);
            _repoMock.Verify(p => p.Insert(It.IsAny<CreateUserRolesCommand>()), Times.Once);
            Assert.True(result.IsSuccess);
        }
        public void Dispose()
        {
        }
    }
}
