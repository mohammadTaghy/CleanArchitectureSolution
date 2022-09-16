using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UserProfileCase.Query.GetUserItem
{
    public class GetUserItemQuery : IRequest<UserItemDto>
    {
        public string UserName { get; set; }
        public int Id { get; set; }
    }
}
