using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IUserRepoRead: IRepositoryReadBase<IUser>
    {
        bool CheckUniqUserName(string userName, int id);

    }
}
