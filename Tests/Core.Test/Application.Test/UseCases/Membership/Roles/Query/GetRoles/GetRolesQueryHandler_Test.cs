using Application.Common.Interfaces;
using Application.UseCases;
using Application.Validation;
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

namespace Application.Test.UseCases
{
    public class GetRolesQueryHandler_Test : UnitTestBase<Membership_Roles, IRolesRepo, IValidationRuleBase<Membership_Roles>>
    {
        private readonly GetRolesQuery _query;
        private readonly GetRolesQueryHandler _handler;

        public GetRolesQueryHandler_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _query = new GetRolesQuery() { PageIndex=1,PageSize=10,SerchText=""};
            _handler = new GetRolesQueryHandler(_repoMock.Object, _mapper.Object);
        }
        [Fact]
        public void GetRolesAsTreeQueryHandler_EmptyRoles_QueryTest()
        {
            int count = 0;
            _repoMock.Setup(p => p.ItemsAsList(It.IsAny<GetRolesQuery>(),out count))
                .Returns(Task.FromResult(new List<RolesDto>()));
            var result = _handler.Handle(_query, CancellationToken.None);
            Assert.True(result.IsCompletedSuccessfully);
            Assert.Equal(new(), result.Result.Result);
            Assert.Equal(CommonMessage.EmptyResponse, result.Result.Message);
            Assert.Equal(count, result.Result.TotalCount);

        }
        [Fact]
        public void GetRolesAsTreeQueryHandler_GetData_QueryTest()
        {
            int count = 1;
            var rolesDto = new RolesDto
            {
                IsAdmin = false,
                RoleName = "test",
            };
            _repoMock.Setup(p => p.ItemsAsList(It.IsAny<GetRolesQuery>(), out count))
                .Returns(Task.FromResult(new List<RolesDto> { rolesDto }));
            var result = _handler.Handle(_query, CancellationToken.None);
            Assert.True(result.IsCompletedSuccessfully);
            Assert.Equal(new List<RolesDto>() { rolesDto }, result.Result.Result);
            Assert.Equal(count, result.Result.TotalCount);

        }
    }
}
