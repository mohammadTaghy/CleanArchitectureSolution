using Application;
using Domain;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class UserRepoRead: RepositoryReadBase<User>, IUserRepoRead
    {
        public UserRepoRead(IConfiguration config):base(config)
        {

        }
        #region Custom Get

        #endregion
        #region Custom Check
        public bool CheckUniqUserName(string userName, int id)
        {
            return Exists(p=>p.UserName==userName&&p.Id!=id);
        }
        #endregion


    }
}
