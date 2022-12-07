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
    public class CurrentUserPermissionsAsTreeQuery : BaseLoadListQuery<QueryResponse<List<PermissionTreeDto>>>
    {
        public int UserId { get; set; }
    }
}
