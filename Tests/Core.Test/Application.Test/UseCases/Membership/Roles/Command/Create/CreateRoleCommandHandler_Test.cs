using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Model;
using Application.UseCases;
using Application.UseCases.UserCase.Command.Create;
using Application.Validation;
using AutoMapper;
using Common;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Application.Test.UseCases
{
    public class CreateRolesCommand_Test : UnitTestBase<Membership_Roles,IRolesRepo,IValidationRuleBase<Membership_Roles>>, IDisposable
    {
        private readonly CreateRoleCommand _createCommand;
        private readonly CreateRoleCommandHandler _handler;

        public CreateRolesCommand_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            
            _createCommand = new CreateRoleCommand
            {
                RoleName = "TestRole",
                IsAdmin =true
            };
            

            _handler = new CreateRoleCommandHandler(_repoMock.Object, _mapper.Object);
        }
        [Fact]
        public void CreateRoleCommand_NullExcption_ResultTest()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));
            Assert.Equal(string.Format(CommonMessage.NullException, "Roles"), exception.Result.Message);
        }
        [Fact]
        public void CreateRoleCommand_IsDuplicateName_ResultTest()
        {
            _repoMock.Setup(p => p.AnyEntity(It.IsAny<Expression<Func<Membership_Roles, bool>>>()))
                .Returns(Task.FromResult(true));
            var exception = Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(_createCommand, CancellationToken.None));
            Assert.Equal(String.Format(CommonMessage.IsDuplicate, "نام نقش"), exception.Result.Failures.First().Value.First());
        }
        [Fact]
        public void CreateRoleCommand_SuccessResult_ResultTest()
        {
            Task<CommandResponse<Membership_Roles>> result = null;
            result = _handler.Handle(_createCommand, CancellationToken.None);
            _repoMock.Verify(p => p.Insert(It.IsAny<Membership_Roles>()), Times.Once);

            Assert.True(result.Result.IsSuccess);
        }
        public void Dispose()
        {
        }
    }
}
