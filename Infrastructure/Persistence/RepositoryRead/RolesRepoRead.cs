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
    public class RolesRepoRead : RepositoryReadBase<Membership_Roles>, IRolesRepoRead
    {
        public RolesRepoRead(IConfiguration config) : base(config)
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
