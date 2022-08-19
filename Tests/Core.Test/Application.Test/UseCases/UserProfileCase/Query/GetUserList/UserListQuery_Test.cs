using Application.UseCases.UserProfileCase.Query.GetUserList;
using AutoMapper;
using Common;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Application.Test.UseCases.UserProfileCase.Query.GetUserList
{
    public class UserListQuery_Test : UnitTestBase
    {
        private readonly Mock<IUserProfileRepo> _userProfileRepo;
        private readonly Mock<IMapper> _mapper;
        private readonly UserListQueryHandler _handler;
        private readonly UserListQuery _userListQueryRequest;

        public UserListQuery_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _userProfileRepo = new Mock<IUserProfileRepo>();
            _mapper = new Mock<IMapper>();
            _handler = new UserListQueryHandler(_userProfileRepo.Object, _mapper.Object);
            _userListQueryRequest = new UserListQuery { 
                FirstName="test",
                Index=1,
                PageSize=10
            };
        }
        [Fact]
        public void UserListQueryHandler_NullRequest_QueryTest()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));
            Assert.Equal(string.Format(CommonMessage.NullException, "Request"), exception.Result.Message);
        }
        [Fact]
        public void UserListQueryHandler_NotErrorIdIndexIsNullOrLessThanZero_QueryTest()
        {
            _userListQueryRequest.Index = -1;
            var result = _handler.Handle(_userListQueryRequest, CancellationToken.None);
            int total = 0;
            _userProfileRepo.Verify(p => p.ItemList(It.IsAny<UserListQuery>(),out total), Times.Once);
            Assert.True(result.IsCompleted);
        }
        [Fact]
        public void UserListQueryHandler_Succes_QueryTest()
        {
            //_userProfileRepo.Setup(p=>p.ItemList(It.IsAny<UserListQueryHandler>()))
            //    .Return()
            var result=_handler.Handle(_userListQueryRequest, CancellationToken.None);
            int total = 0;
            _userProfileRepo.Verify(p => p.ItemList(It.IsAny<UserListQuery>(), out total), Times.Once);
            Assert.True(result.IsCompleted);
        }
    }
}
