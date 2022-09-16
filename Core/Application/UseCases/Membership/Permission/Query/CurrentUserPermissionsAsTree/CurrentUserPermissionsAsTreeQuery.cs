using Application.Common.Model;
using Application.Mappings;
using Application.UseCases.UserProfileCase.Query.GetUserItem;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class CurrentUserPermissionsAsTreeQuery : IRequest<QueryResponse<List<PermissionTreeDto>>>
    {
        public CurrentUserPermissionsAsTreeQuery() { }
        public CurrentUserPermissionsAsTreeQuery(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; set; }
    }
}
