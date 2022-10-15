using Application.UseCases.UserProfileCase.Query.GetUserItem;
using Application.UseCases.UserProfileCase.Query.GetUserList;
using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IUserProfileRepo : IRepositoryBase<Membership_UserProfile>
    {
        Task<List<Membership_UserProfile>> ItemList(UserListQuery userListQueryHandler, out int total);
        Task<Membership_UserProfile> FindAsync(int id, string userName, CancellationToken cancellationToken);
    }
}
