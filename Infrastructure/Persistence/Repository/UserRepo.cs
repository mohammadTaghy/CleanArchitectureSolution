using Application;
using Common;
using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class UserRepo: RepositoryBase<Membership_User>,IUserRepo
    {
        public UserRepo(PersistanceDBContext context, ICurrentUserSession currentUserSession) :base(context,currentUserSession)
        {
            
        }
        public async Task<bool> AnyEntity(Membership_User user)
        {
            return await base.AnyEntity(p=>p.UserName==user.UserName);
        }

        public async Task<Membership_User> FindAsync(int? id, string userName, CancellationToken cancellationToken)
        {
            return await base.FindAsync(p => p.Id == id || p.UserName == userName, cancellationToken);
        }

        
    }
}
