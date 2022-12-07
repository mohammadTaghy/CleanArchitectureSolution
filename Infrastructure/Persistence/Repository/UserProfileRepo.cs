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

        public Task<List<Membership_UserProfile>> ItemList(UserListQuery userListQuery, out int total)
        {
            //int index = userListQuery.Index - 1;
            //if (index < 0)
            //    index = 0;
            //var query = GetAllAsQueryable();
            //if(!string.IsNullOrEmpty(userListQuery.UserName))
            //    query=query.Where(p=>p.User.UserName.Contains(userListQuery.UserName));
            //if (!string.IsNullOrEmpty(userListQuery.FirstName))
            //    query = query.Where(p => p.FirstName.Contains(userListQuery.FirstName));
            //if (!string.IsNullOrEmpty(userListQuery.LastName))
            //    query = query.Where(p => p.LastName.Contains(userListQuery.LastName));
            //if (!string.IsNullOrEmpty(userListQuery.MobileNumber))
            //    query = query.Where(p => p.User.MobileNumber.Contains(userListQuery.MobileNumber));
            //if (!string.IsNullOrEmpty(userListQuery.NationalCode))
            //    query = query.Where(p => p.NationalCode.Contains(userListQuery.NationalCode));

            //total =query.Count();
            //return Task.FromResult(query.Skip(index*userListQuery.PageSize).Take(userListQuery.PageSize).ToList());
            total = 0;
            return null;
        }
    }
}
