﻿using Application.Common.Exceptions;
using Application.UseCases.UserCase.Query.SignIn;
using Application.UseCases.UserProfileCase.Query.GetUserItem;
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

namespace Application.Test.UseCases.UserCase.Query.SignIn
{
    public class SignInQuery_Test : UnitTestBase
    {
        private readonly Mock<IUserRepo> _userRepo;
        private readonly Mock<IMapper> _mapper;
        private readonly SignInQueryHandler _handler;
        private readonly SignInQuery _signInQuery;
        public SignInQuery_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _userRepo = new Mock<IUserRepo>();
            _mapper = new Mock<IMapper>();
            _handler = new SignInQueryHandler(_userRepo.Object, _mapper.Object);
            _signInQuery = new SignInQuery
            {
                UserName = "test",
                Password = "1234",
            };
        }
        [Fact]
        public void SignIn_CorrectInfo_QueryTest()
        {
            _userRepo.Setup(p => p.FindAsync(It.IsAny<int?>(), It.IsAny<string>(), CancellationToken.None))
                .Returns(Task.FromResult(new User()
                {
                    UserName = _signInQuery.UserName,
                    PasswordHash = UtilizeFunction.CreateMd5("1234"),
                    Id = 1
                }));
            var result = _handler.Handle(_signInQuery, CancellationToken.None);
            Assert.NotNull(result);
            Assert.True(result.IsCompletedSuccessfully);
            Assert.Equal(1, result.Result);
        }
        [Fact]
        public void SignIn_InCorrectInfo_QueryTest()
        {
            _userRepo.Setup(p => p.FindAsync(It.IsAny<int?>(), It.IsAny<string>(), CancellationToken.None))
                .Returns(Task.FromResult(new User()
                {
                    UserName = _signInQuery.UserName,
                    PasswordHash = UtilizeFunction.CreateMd5("12345"),
                    Id = 1
                }));
            var result = _handler.Handle(_signInQuery, CancellationToken.None);
            Assert.True(result.IsFaulted);
            Assert.Equal(new NotFoundException(_signInQuery.UserName,_signInQuery.Password).Message, result.Exception.GetBaseException().Message);
        }
    }
}
