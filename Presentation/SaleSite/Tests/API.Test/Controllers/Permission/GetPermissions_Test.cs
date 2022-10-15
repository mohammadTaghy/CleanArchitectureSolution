using API.Controllers;
using Application;
using Application.Common.Interfaces;
using Application.Common.Model;
using Application.UseCases;
using Common;
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
    public class GetPermissions_Test : BaseController_Test
    {
        private readonly Mock<ICurrentUserService> _currentUserService;
        private readonly PermissionController _controller;
        private readonly PermissionsAsTreeQuery _permissionsAsTreeQuery;

        public GetPermissions_Test()
        {
            _currentUserService = new Mock<ICurrentUserService>();
            _controller = new PermissionController(_mediator.Object, _currentUserService.Object);
            _permissionsAsTreeQuery = new PermissionsAsTreeQuery { RoleId = 1 };
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
            var result = await _controller.GetPermissions(_permissionsAsTreeQuery, CancellationToken.None);
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(new List<PermissionTreeDto>{ permissionDto }, result.Result);
        }
        [Fact]
        public async Task GetPermissionsAPI_CannotGetData_Test()
        {

            _mediator.Setup(p => p.Send(It.IsAny<IRequest<QueryResponse<List<PermissionTreeDto>>>>(), CancellationToken.None))
                .Returns(Task.FromResult(QueryResponse<List<PermissionTreeDto>>.CreateInstance(
                    null, CommonMessage.EmptyResponse, 0, false
                )));
            _currentUserService.Setup(p => p.UserId).Returns(1);
            var result = await _controller.GetPermissions(_permissionsAsTreeQuery, CancellationToken.None);
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Null(result.Result);
        }
    }
}
