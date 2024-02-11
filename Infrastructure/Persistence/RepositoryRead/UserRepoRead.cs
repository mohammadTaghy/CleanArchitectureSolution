using Application;
using Application.Common;
using Common;
using Domain;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class UserRepoRead: RepositoryReadBase<Membership_User>, IUserRepoRead
    {
        public UserRepoRead(IOptions<MongoDatabaseOption> config, IDirectExchangeRabbitMQ directExchangeRabbitMQ):base(config, directExchangeRabbitMQ)
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
