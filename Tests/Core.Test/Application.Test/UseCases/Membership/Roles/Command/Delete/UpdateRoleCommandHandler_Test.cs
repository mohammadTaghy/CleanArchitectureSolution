﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test.UseCases.Membership.Roles.Command.Delete
{
    public class UpdateRoleCommandHandler_Test : UnitTestBase<Membership_Roles, IRolesRepo>
    {
        private readonly UpdateRoleCommand _updateCommand;
        private readonly UpdateRoleCommandHandler _handler;

        public UpdateRoleCommandHandler_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {

            _updateCommand = new UpdateRoleCommand
            {
                Name = "TestRole",
                IsAdmin = true
            };


            _handler = new UpdateRoleCommandHandler(_repoMock.Object, _mapper, _cacheManager.Object);
        }
        [Fact]
        public void updateRoleCommand_NullExcption_ResultTest()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));

            _repoMock.Verify(p => p.Update(It.IsAny<UpdateRoleCommand>()), Times.Never);
            Assert.Equal(string.Format(CommonMessage.NullException, "Roles"), exception.Result.Message);
        }
        [Fact]
        public async Task updateRoleCommand_NotFoundException_ResultTest()
        {
            _repoMock.Setup(p => p.AnyEntity(It.IsAny<Expression<Func<Membership_Roles, bool>>>()))
                .Returns(Task.FromResult(false));

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(_updateCommand, CancellationToken.None));

            _repoMock.Verify(p => p.Update(It.IsAny<UpdateRoleCommand>()), Times.Never);
            Assert.Equal(String.Format(CommonMessage.NotFound, "Role"), exception.Message);
        }
        [Fact]
        public void updateRoleCommand_SuccessResult_ResultTest()
        {
            Task<CommandResponse<Membership_Roles>> result = null;
            _repoMock.Setup(p => p.AnyEntity(It.IsAny<Expression<Func<Membership_Roles, bool>>>()))
                .Returns(Task.FromResult(true));

            result = _handler.Handle(_updateCommand, CancellationToken.None);

            _repoMock.Verify(p => p.Update(It.IsAny<UpdateRoleCommand>()), Times.Once);
            Assert.True(result.Result.IsSuccess);
        }
    }
}
