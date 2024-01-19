using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IRolesRepoRead: IRepositoryReadBase<Membership_Roles>
    {
        bool CheckUniqRolesName(string RolesName, int id);

    }
}
