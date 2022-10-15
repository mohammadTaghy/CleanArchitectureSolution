using Application.Common.Exceptions;
using Application.Common.Model;
using Application.Mappings;
using Application.UseCases.UserCase.Command.Create;
using Application.Validation;
using AutoMapper;
using Common;
using Domain;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Application.Test.UseCases.UserCase.Command.Create
{
    public class CreateUserCommand_Test : UnitTestBase<Membership_User,IUserRepo,IUserValidation>, IDisposable
    {
       
        private readonly CreateUserCommand _createCommand;
        private readonly List<Membership_User> _users;
        private readonly CreateUserCommandHandler _handler;

        public CreateUserCommand_Test(ITestOutputHelper testOutputHelper):base(testOutputHelper)
        {
           
            _createCommand = new CreateUserCommand
            {
                UserName = "MTY",
                MobileNumber = "0938776767",
                Email="taghy@gmail.com",
                Password="123456"
            };
            _users = new List<Membership_User>() { new Membership_User {
                Id=1,
                Email=_createCommand.Email,
                UserName=_createCommand.UserName,
                MobileNumber=_createCommand.MobileNumber,
            } };
            
            _handler = new CreateUserCommandHandler(_validationMock.Object, _repoMock.Object, _mapper.Object);
        }
        [Fact]
        public void CreateUserCommandHandler_NullExcption_ResultTest()
        {
            
            var exception = Assert.ThrowsAsync<ArgumentNullException>(()=>_handler.Handle(null, CancellationToken.None));
            Assert.Equal(string.Format(CommonMessage.NullException, "User"),exception.Result.Message);
        }
        [Fact]
        public void CreateUserCommandHandler_CheckExistUser_ResultTest()
        {
            bool existUser=false;
            _repoMock.Setup(p => p.AnyEntity(It.IsAny<Membership_User>()))
                .Returns(Task.FromResult<bool>(true));
            _mapper.Setup(p => p.Map<Membership_User>(It.IsAny<CreateUserCommand>()))
                .Returns(new Membership_User
                {
                    UserName = "MTY",
                    MobileNumber = "0938776767",
                    Email = "taghy@gmail.com",
                    PasswordHash = "123456"
                });
            var exception = Assert.ThrowsAsync<ValidationException>(()=> _handler.Handle(_createCommand, CancellationToken.None));
            _repoMock.Verify(p => p.Insert(It.IsAny<Membership_User>()), Times.Never);
            Assert.Equal(String.Format(CommonMessage.IsDuplicateUserName, _createCommand.UserName), exception.Result.Failures.First().Value.First());
        }
        [Fact]
        public void CreateUserCommandHandler_SuccessResult_ResultTest()
        {
            Task<CommandResponse<int>> result = null;
            _mapper.Setup(p => p.Map<Membership_User>(It.IsAny<CreateUserCommand>()))
                .Returns(new Membership_User
                {
                    UserName = "MTY",
                    MobileNumber = "0938776767",
                    Email = "taghy@gmail.com",
                    PasswordHash = "123456"
                });
            _repoMock.Setup(p => p.AnyEntity(It.IsAny<Membership_User>()))
                .Returns(Task.FromResult<bool>(false));
            result = _handler.Handle(_createCommand, CancellationToken.None);
            _repoMock.Verify(p => p.Insert(It.IsAny<Membership_User>()), Times.Once);

            Assert.True(result.Result.IsSuccess);
        }

        public void Dispose()
        {

        }
    }
}
