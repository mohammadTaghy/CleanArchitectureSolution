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
    public class CreatePermissionCommand_Test : UnitTestBase<Permission,IPermissionRepo,IValidationRuleBase<Permission>>, IDisposable
    {
        private readonly CreatePermissionCommand _createCommand;
        private readonly CreatePermissionCommandHandler _handler;

        public CreatePermissionCommand_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            
            _createCommand = new CreatePermissionCommand
            {
                CommandName = "CommandTest",
                FeatureType =(byte) Constants.FeatureType.Command,
                IsActive = true,
                Name = "Test",
                Title="test"
            };
            

            _handler = new CreatePermissionCommandHandler(_repoMock.Object, _mapper.Object);
        }
        [Fact]
        public void CreatePermissionCommand_NullExcption_ResultTest()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));
            Assert.Equal(string.Format(CommonMessage.NullException, "Permission"), exception.Result.Message);
        }
        [Fact]
        public void CreatePermissionCommand_SuccessResult_ResultTest()
        {
            Task<CommandResponse<Permission>> result = null;
            result = _handler.Handle(_createCommand, CancellationToken.None);
            _repoMock.Verify(p => p.Insert(It.IsAny<Permission>()), Times.Once);

            Assert.True(result.Result.IsSuccess);
        }
        public void Dispose()
        {
        }
    }
}
