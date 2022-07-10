﻿using Application.UseCases.UserProfileCase.Query.GetUserItem;
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

namespace Application.Test.UseCases.UserProfileCase.Query.GetUserItem
{
    public class GetUserItem_Test : UnitTestBase
    {
        private readonly Mock<IUserProfileRepo> _userProfileRepo;
        private readonly Mock<IMapper> _mapper;
        private readonly GetUserItemQueryHandler _handler;
        private readonly GetUserItemQuery _userListQueryRequest;

        public GetUserItem_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _userProfileRepo = new Mock<IUserProfileRepo>();
            _mapper = new Mock<IMapper>();
            _handler = new GetUserItemQueryHandler(_userProfileRepo.Object, _mapper.Object);
            _userListQueryRequest = new GetUserItemQuery
            {
                UserName = "test",
                Id = 1,
            };
        }
        [Fact]
        public void GetUserItem_NullRequest_QueryTest()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));
            Assert.Equal(string.Format(CommonMessage.NullException, "Request"), exception.Result.Message);
        }
        [Fact]
        public void GetUserItem_Succes_QueryTest()
        {
            //_userProfileRepo.Setup(p=>p.ItemList(It.IsAny<UserListQueryHandler>()))
            //    .Return()
            var result = _handler.Handle(_userListQueryRequest, CancellationToken.None);
            int total = 0;
            _userProfileRepo.Verify(p => p.FindAsync(It.IsAny<int>(),It.IsAny<string>(),CancellationToken.None), Times.Once);
            Assert.True(result.IsCompleted);
        }
    }
}
