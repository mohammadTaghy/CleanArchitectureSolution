//using Application.Common.Interfaces;
//using Application.IRepository;
//using Application.UseCases;
//using Application.Validation;
//using Common;
//using Domain.Entities;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using Xunit;
//using Xunit.Abstractions;

//namespace Application.Test.UseCases.PermissionUseCase.Query.GetPermissionAsTree
//{
//    public class GetPermissionAsTreeQueryHandler_Test : UnitTestBase<RolesPermission, IRolesPermissionRepo, IValidationRuleBase<RolesPermission>>
//    {
//        private readonly GetPermissionAsTreeQuery _query;
//        private readonly GetPermissionAsTreeQueryHandler _handler;

//        public GetPermissionAsTreeQueryHandler_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
//        {
//            _query = new GetPermissionAsTreeQuery(1);
//            _handler = new GetPermissionAsTreeQueryHandler(_repoMock.Object, _mapper.Object);
//        }
//        [Fact]
//        public void GetPermissionAsTreeQueryHandler_EmptyPermission_QueryTest()
//        {
//            _repoMock.Setup(p => p.GetCurrentRolePermissions(It.IsAny<int>()))
//                .Returns(Task.FromResult(new List<PermissionTreeDto>()));
//            var result = _handler.Handle(_query,CancellationToken.None);
//            Assert.True(result.IsCompletedSuccessfully);
//            Assert.Equal(new (), result.Result.Result);
//            Assert.Equal(CommonMessage.Unauthorized, result.Result.Message);
//            Assert.Equal(0,result.Result.TotalCount);

//        }
//        [Fact]
//        public void GetPermissionAsTreeQueryHandler_GetData_QueryTest()
//        {
//            var permissionDto = new PermissionTreeDto
//            {
//                HasChild = false,
//                Id = 1,
//                Name = "Membership",
//                Title = "مدیریت کاربران"
//            };
//            _repoMock.Setup(p => p.GetCurrentRolePermissions(It.IsAny<int>()))
//                .Returns(Task.FromResult(new List<PermissionTreeDto> { permissionDto }));
//            var result = _handler.Handle(_query, CancellationToken.None);
//            Assert.True(result.IsCompletedSuccessfully);
//            Assert.Equal(new List<PermissionTreeDto>() { permissionDto }, result.Result.Result);
//            Assert.Equal(1, result.Result.TotalCount);

//        }
//    }
//}
