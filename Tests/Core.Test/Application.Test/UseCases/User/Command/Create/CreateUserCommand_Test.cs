using Application.Mappings;
using Application.UseCases.User.Command.Create;
using Application.Validation;
using AutoMapper;
using Common;
using Domain;
using Moq;
using System;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace Application.Test.UseCases.User.Command.Create
{
    public class CreateUserCommand_Test : UnitTestBase, IDisposable
    {
        private readonly IUserValidation _userValidation;
        private readonly Mock<IUserRepo> _userRepo;
        private readonly IMapper _mapper;

        public CreateUserCommand_Test(ITestOutputHelper testOutputHelper):base(testOutputHelper)
        {
            _userRepo = new Mock<IUserRepo>();
            _userValidation = Mock.Of<IUserValidation>(x => x.UserRepo == _userRepo.Object);
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }
        
        [Fact]
        public void CreateUserCommandHandler_GiveCommand_ResultTest()
        {
            IUser user = Mock.Of<IUserRead>();
            user.UserName = "MTY";
            _userRepo.Setup(x => x.Insert(It.IsAny<IUserRead>())).Returns(user);
            var handler =new CreateUserCommandHandler(_userValidation, _userRepo.Object,_mapper);
            var createCommand = new CreateUserCommand { 
                UserName="MTY", 
                FirstName="Taghy", 
                LastName="Yami", 
                NationalCode="0922", 
                MobileNumber="0938776767" 
            };
            var result = handler.Handle(createCommand, CancellationToken.None);

            Assert.True(result.Result.IsSuccess);
            Assert.Equal(createCommand.UserName, user.UserName, ignoreCase: true);
        }

        public void Dispose()
        {

        }
    }
}
