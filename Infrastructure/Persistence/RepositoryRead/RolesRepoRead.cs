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
    public class RolesRepoRead : RepositoryReadBase<Membership_Roles>, IRolesRepoRead
    {
        public RolesRepoRead(IOptions<MongoDatabaseOption> config, IDirectExchangeRabbitMQ directExchangeRabbitMQ) : base(config, directExchangeRabbitMQ)
        {

        }
        #region Custom Get

        #endregion
        #region Custom Check
        public bool CheckUniqRolesName(string RoleName, int id)
        {
            return Exists(p => p.Name == RoleName && p.Id != id);
        }
        #endregion


    }
}
