using Application.Common.Interfaces;
using Application.UseCases;
using Common;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit;
using Application.Common.Exceptions;

namespace Application.Test.UseCases
{
    public class GetRoleItemQueryHandler_Test : UnitTestBase<Membership_Roles, IRolesRepo, IValidationRuleBase<Membership_Roles>>
    {
        private readonly GetRoleItemQuery _query;
        private readonly GetRoleItemQueryHandler _handler;

        public GetRoleItemQueryHandler_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _query = new GetRoleItemQuery() { Id = 1 };
            _handler = new GetRoleItemQueryHandler(_repoMock.Object, _mapper.Object);
        }
        [Fact]
        public void GetRoleItemQueryHandler_NotFind_QueryTest()
        {
            Membership_Roles roles = null;
            _repoMock.Setup(p => p.FindAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(roles));
            var result = Assert.ThrowsAnyAsync<NotFoundException>(()=>_handler.Handle(_query, CancellationToken.None));
            Assert.Equal(String.Format(CommonMessage.NotFound, $"نقش با کد رایانه {_query.Id}"), 
                result.Result.Message);

        }
        [Fact]
        public void GetRolesAsTreeQueryHandler_GetData_QueryTest()
        {
            var roles = new Membership_Roles
            {
                IsAdmin = false,
                RoleName = "test",
            };
            var rolesDto = new RolesDto
            {
                IsAdmin = false,
                RoleName = "test",
            };
            _repoMock.Setup(p => p.FindAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(roles));
            _mapper.Setup(p=>p.Map<RolesDto>(It.IsAny<Membership_Roles>()))
            .Returns(rolesDto);
            var result = _handler.Handle(_query, CancellationToken.None);
            Assert.True(result.Result.IsSuccess);
            Assert.Equal( rolesDto, result.Result.Result);

        }
    }
}
