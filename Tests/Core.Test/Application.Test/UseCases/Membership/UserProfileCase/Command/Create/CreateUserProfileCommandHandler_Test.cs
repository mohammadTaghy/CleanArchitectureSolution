using Application.Common.Interfaces;
using Application.Common.Model;
using Application.Mappings;
using Application.UseCases.UserProfileCase.Command.Create;
using Application.Validation;
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
    public class CreateUserProfileCommandHandler_Test : UnitTestBase<Membership_UserProfile, IUserProfileRepo, IValidationRuleBase<Membership_UserProfile>>, IDisposable
    {
        private readonly Mock<IUserRepo> _userRepo;
        private readonly CreateUserProfileCommand _createCommand;
        private readonly List<Membership_UserProfile> _userProfile;
        private readonly CreateUserProfileCommandHandler _handler;
        private delegate void MockAnyEntityCallback(string number, ref int output);

        public CreateUserProfileCommandHandler_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _userRepo = new Mock<IUserRepo>();
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
                Id=1,
                BirthDate="1368/03/21",
            };
            
            _handler = new CreateUserProfileCommandHandler(_repoMock.Object,_userRepo.Object, _mapper.Object);
        }
        [Fact]
        public void CreateUserCommandHandler_NullExcption_ResultTest()
        {

            var exception = Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));
            _repoMock.Verify(p => p.Insert(It.IsAny<Membership_UserProfile>()), Times.Never);
            Assert.Equal(string.Format(CommonMessage.NullException, "UserProfile"), exception.Result.Message);
        }
        [Fact]
        public void CreateUserCommandHandler_ArgumentNullException_ResultTest()
        {
            _createCommand.FirstName = null;
            _createCommand.LastName = null;
            _createCommand.MobileNumber = null;
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(_createCommand, CancellationToken.None));
            _repoMock.Verify(p => p.Insert(It.IsAny<Membership_UserProfile>()), Times.Never);
            Assert.Equal(String.Format(CommonMessage.NullException,
                $"{nameof(CreateUserProfileCommand.FirstName)}-{nameof(CreateUserProfileCommand.LastName)}-{nameof(CreateUserProfileCommand.MobileNumber)}"), 
                exception.Result.Message);
        }
        [Fact]
        public void CreateUserCommandHandler_IfUserNotExistUserMustBeInsert_ResultTest()
        {
            _userRepo.Setup(p => p.FindAsync(It.IsAny<int>(), It.IsAny<string>(), CancellationToken.None))
                .Returns( Task.FromResult<Membership_User>(null));
            _userRepo.Setup(p => p.Insert(It.IsAny<Membership_User>()))
               .Returns(Task.FromResult(new Membership_User { Id=1}));
            _mapper.Setup(p => p.Map<Membership_UserProfile>(It.IsAny<CreateUserProfileCommand>()))
               .Returns(new Membership_UserProfile
               {
                   FirstName = "Taghy",
                   LastName = "Yami",
                   Id = 1,
                   BirthDate = DateTimeHelper.ToDateTime("1368/03/21")
               });
            Task<CommandResponse<int>> result = _handler.Handle(_createCommand, CancellationToken.None);
            _repoMock.Verify(p => p.Insert(It.IsAny<Membership_UserProfile>()), Times.Never);
            _userRepo.Verify(p => p.Insert(It.IsAny<Membership_User>()), Times.Once);
            Assert.True(result.IsCompleted);
            Assert.True(result.Result.IsSuccess);
        }
        [Fact]
        public void CreateUserCommandHandler_IfUserExistInsertProfile_ResultTest()
        {
            _userRepo.Setup(p => p.FindAsync(It.IsAny<int>(), It.IsAny<string>(), CancellationToken.None))
                 .Returns(Task.FromResult<Membership_User>(new Membership_User { Id = 1 }));
            _repoMock.Setup(p => p.Insert(It.IsAny<Membership_UserProfile>()))
               .Returns(Task.FromResult(new Membership_UserProfile { Id = 1 }));
            _mapper.Setup(p => p.Map<Membership_UserProfile>(It.IsAny<CreateUserProfileCommand>()))
                .Returns(new Membership_UserProfile
                {
                    FirstName = "Taghy",
                    LastName = "Yami",
                    Id = 1,
                    BirthDate = DateTimeHelper.ToDateTime("1368/03/21")
                });
            Task<CommandResponse<int>> result = _handler.Handle(_createCommand, CancellationToken.None);
            _repoMock.Verify(p => p.Insert(It.IsAny<Membership_UserProfile>()), Times.Once);
            _userRepo.Verify(p => p.Insert(It.IsAny<Membership_User>()), Times.Never);
            Assert.True(result.IsCompleted);
            Assert.True(result.Result.IsSuccess);
        }

        public void Dispose()
        {

        }
    }
}
