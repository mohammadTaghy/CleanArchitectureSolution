using Application;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    internal class UserRepo: RepositoryBase<IUser>,IUserRepo
    {
        public UserRepo(PersistanceDBContext context):base(context)
        {

        }
    }
}
