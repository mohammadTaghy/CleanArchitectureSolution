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
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Application.Test.UseCases
{
    
    public class CreateRolesPermissionCommand_Test : UnitTestBase<RolesPermission, IRolesPermissionRepo, IValidationRuleBase<RolesPermission>>, IDisposable
    {
        private readonly CreateRolesPermissionCommand _createCommand;
        private readonly CreateRolesPermissionCommandHandler _handler;

        public CreateRolesPermissionCommand_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            
            _createCommand = new CreateRolesPermissionCommand
            {
                RolesId = 1,
                PermissionIds=new List<int> { 1, 2 },
            };
            

            _handler = new CreateRolesPermissionCommandHandler(_repoMock.Object, _mapper.Object);
        }
        [Fact]
        public void CreateRolesPermissionCommand_NullExcption_ResultTest()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));
            Assert.Equal(string.Format(CommonMessage.NullException, "RolesPermission"), exception.Result.Message);
        }
        [Fact]
        public void CreateRolesPermissionCommand_HasError_ResultTest()
        {

            _repoMock.Setup(p => p.Insert(It.IsAny<CreateRolesPermissionCommand>()))
                .Returns(Task.FromResult(false));
            var exception = Assert.ThrowsAsync<Exception>(()=> _handler.Handle(_createCommand, CancellationToken.None));
            _repoMock.Verify(p => p.Insert(It.IsAny<CreateRolesPermissionCommand>()), Times.Once);
            Assert.Equal(CommonMessage.Error, exception.Result.Message);
        }
        [Fact]
        public async void CreateRolesPermissionCommand_SuccessResult_ResultTest()
        {
            
            _repoMock.Setup(p=>p.Insert(It.IsAny<CreateRolesPermissionCommand>()))
                .Returns(Task.FromResult(true));
            var result = await _handler.Handle(_createCommand, CancellationToken.None);
            _repoMock.Verify(p => p.Insert(It.IsAny<CreateRolesPermissionCommand>()), Times.Once);
            Assert.True(result.IsSuccess);
        }
        public void Dispose()
        {
        }
    }
}
