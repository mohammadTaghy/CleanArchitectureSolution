using API.Controllers;
using Application;
using Application.Common.Interfaces;
using Application.Common.Model;
using Application.UseCases;
using Domain.Entities;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace API.Test.Controllers
{
    public class GetPermissions_Test : BaseController_Test<PermissionController>
    {
        public GetPermissions_Test(ITestOutputHelper testOutputHelper)
        {

        }
        [Fact]
        public async Task GetPermissionsAPI_GetData_Test()
        {
            var permissionDto = new PermissionTreeDto
            {
                HasChild = false,
                Id = 1,
                Name = "Membership",
                Title = "مدیریت کاربران"
            };
            _mediator.Setup(p => p.Send(It.IsAny<IRequest<QueryResponse<List<PermissionTreeDto>>>>(), CancellationToken.None))
                .Returns(Task.FromResult(QueryResponse<List<PermissionTreeDto>>.CreateInstance(
                new List<PermissionTreeDto>
                    {
                        permissionDto
                    },"",1,true
                )));
            var result = await _controller.GetPermissions(CancellationToken.None);
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(new List<PermissionTreeDto>{ permissionDto }, result.Result);
        }
    }
}
