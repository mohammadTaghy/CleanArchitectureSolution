using Application;
using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class UserRepo: RepositoryBase<User>,IUserRepo
    {
        public UserRepo(PersistanceDBContext context):base(context)
        {
            
        }
        public async Task<bool> AnyEntity(User user)
        {
            return await base.AnyEntity(p=>p.UserName==user.UserName);
        }

        public async Task<User> FindAsync(int? id, string userName, CancellationToken cancellationToken)
        {
            return await base.FindAsync(p => p.Id == id || p.UserName == userName, cancellationToken);
        }

        
    }
}
