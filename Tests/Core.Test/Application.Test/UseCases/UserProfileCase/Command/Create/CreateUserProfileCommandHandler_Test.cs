using Application.Common.Model;
using Application.Mappings;
using Application.UseCases.UserProfileCase.Command.Create;
using AutoMapper;
using Common;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Application.Test.UseCases.UserProfileCase.Command.Create
{
    public class CreateUserProfileCommandHandler_Test : UnitTestBase, IDisposable
    {
        private readonly Mock<IUserProfileRepo> _userProfileRepo;
        private readonly Mock<IUserRepo> _userRepo;
        private readonly CreateUserProfileCommand _createCommand;
        private readonly List<UserProfile> _userProfile;
        private readonly IMapper _mapper;
        private readonly CreateUserProfileCommandHandler _handler;
        private delegate void MockAnyEntityCallback(string number, ref int output);

        public CreateUserProfileCommandHandler_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _userRepo = new Mock<IUserRepo>();
            _userProfileRepo = new Mock<IUserProfileRepo>();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _createCommand = new CreateUserProfileCommand
            {

                UserName = "MTY",
                MobileNumber = "0938776767",
                Email = "taghy@gmail.com",
                Password = "123456",
                FirstName="Taghy",
                LastName="Yami",
                UserId=1,
                BirthDate="1368/03/21",
            };
            
            _mapper = configurationProvider.CreateMapper();
            _handler = new CreateUserProfileCommandHandler(_userProfileRepo.Object,_userRepo.Object, _mapper);
        }
        [Fact]
        public void CreateUserCommandHandler_NullExcption_ResultTest()
        {

            var exception = Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));
            _userProfileRepo.Verify(p => p.Insert(It.IsAny<UserProfile>()), Times.Never);
            Assert.Equal(string.Format(CommonMessage.NullException, "UserProfile"), exception.Result.Message);
        }
        [Fact]
        public void CreateUserCommandHandler_ArgumentNullException_ResultTest()
        {
            _createCommand.FirstName = null;
            _createCommand.LastName = null;
            _createCommand.MobileNumber = null;
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(_createCommand, CancellationToken.None));
            _userProfileRepo.Verify(p => p.Insert(It.IsAny<UserProfile>()), Times.Never);
            Assert.Equal(String.Format(CommonMessage.NullException,
                $"{nameof(CreateUserProfileCommand.FirstName)}-{nameof(CreateUserProfileCommand.LastName)}-{nameof(CreateUserProfileCommand.MobileNumber)}"), 
                exception.Result.Message);
        }
        [Fact]
        public void CreateUserCommandHandler_IfUserNotExistUserMustBeInsert_ResultTest()
        {
            _userRepo.Setup(p => p.FindAsync(It.IsAny<int>(), It.IsAny<string>(), CancellationToken.None))
                .Returns(() => Task.FromResult<User>(null));
            _userRepo.Setup(p => p.Insert(It.IsAny<User>()))
               .Returns(() => new User { Id=1});
            Task<CommandResponse<int>> result = _handler.Handle(_createCommand, CancellationToken.None);
            _userProfileRepo.Verify(p => p.Insert(It.IsAny<UserProfile>()), Times.Never);
            _userRepo.Verify(p => p.Insert(It.IsAny<User>()), Times.Once);
            Assert.True(result.IsCompleted);
            Assert.True(result.Result.IsSuccess);
        }
        [Fact]
        public void CreateUserCommandHandler_IfUserExistInsertProfile_ResultTest()
        {
            _userRepo.Setup(p => p.FindAsync(It.IsAny<int>(), It.IsAny<string>(), CancellationToken.None))
                 .Returns(() => Task.FromResult<User>(new User { Id = 1 }));
            _userProfileRepo.Setup(p => p.Insert(It.IsAny<UserProfile>()))
               .Returns(() => new UserProfile { Id = 1 });
            Task<CommandResponse<int>> result = _handler.Handle(_createCommand, CancellationToken.None);
            _userProfileRepo.Verify(p => p.Insert(It.IsAny<UserProfile>()), Times.Once);
            _userRepo.Verify(p => p.Insert(It.IsAny<User>()), Times.Never);
            Assert.True(result.IsCompleted);
            Assert.True(result.Result.IsSuccess);
        }

        public void Dispose()
        {

        }
    }
}
