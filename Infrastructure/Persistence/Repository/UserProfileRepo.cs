using Application;
using Application.UseCases.UserProfileCase.Query.GetUserList;
using Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class UserProfileRepo : RepositoryBase<Membership_UserProfile>, IUserProfileRepo
    {
        public UserProfileRepo(PersistanceDBContext context, ICurrentUserSession currentUserSession) : base(context,currentUserSession)
        {

        }

        public Task<Membership_UserProfile> FindAsync(int id, string userName,CancellationToken cancellationToken)
        {
            return base.FindAsync(p => p.Id == id || p.User.UserName == userName,cancellationToken);
        }

        public Task<List<Membership_UserProfile>> ItemList(UserListQuery userListQueryHandler, out int total)
        {
            var query = GetAllAsQueryable();
            if(!string.IsNullOrEmpty(userListQueryHandler.UserName))
                query=query.Where(p=>p.User.UserName.Contains(userListQueryHandler.UserName));
            total=query.Count();
            return Task.FromResult(query.ToList());
        }
    }
}
