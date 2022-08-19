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
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Application.Test.UseCases.UserCase.Command.Create
{
    public class CreateUserCommand_Test : UnitTestBase, IDisposable
    {
        private readonly IUserValidation _userValidation;
        private readonly Mock<IUserRepo> _userRepo;
        private readonly CreateUserCommand _createCommand;
        private readonly List<User> _users;
        private readonly IMapper _mapper;
        private readonly CreateUserCommandHandler _handler;
        private delegate void MockAnyEntityCallback(string number, ref int output);

        public CreateUserCommand_Test(ITestOutputHelper testOutputHelper):base(testOutputHelper)
        {
            _userRepo = new Mock<IUserRepo>();
            _userValidation = Mock.Of<IUserValidation>(x => x.UserRepo == _userRepo.Object);
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _createCommand = new CreateUserCommand
            {
                UserName = "MTY",
                MobileNumber = "0938776767",
                Email="taghy@gmail.com",
                Password="123456"
            };
            _users = new List<User>() { new User {
                Id=1,
                Email=_createCommand.Email,
                UserName=_createCommand.UserName,
                MobileNumber=_createCommand.MobileNumber,
            } };
            
            _mapper = configurationProvider.CreateMapper();
            _handler = new CreateUserCommandHandler(_userValidation, _userRepo.Object, _mapper);
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
            _userRepo.Setup(p => p.AnyEntity(It.IsAny<User>()))
                .Returns(Task.FromResult<bool>(true));
            var exception = Assert.ThrowsAsync<Exception>(()=> _handler.Handle(_createCommand, CancellationToken.None));
            _userRepo.Verify(p => p.Insert(It.IsAny<User>()), Times.Never);
            Assert.Equal(String.Format(CommonMessage.IsDuplicateUserName, _createCommand.UserName), exception.Result.Message);
        }
        [Fact]
        public void CreateUserCommandHandler_SuccessResult_ResultTest()
        {
            Task<CommandResponse<int>> result = null;
            result = _handler.Handle(_createCommand, CancellationToken.None);
            _userRepo.Verify(p => p.Insert(It.IsAny<User>()), Times.Once);

            Assert.True(result.Result.IsSuccess);
        }

        public void Dispose()
        {

        }
    }
}
