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
    internal class UserRepo: RepositoryBase<User>,IUserRepo
    {
        public UserRepo(PersistanceDBContext context):base(context)
        {

        }
    }
}
