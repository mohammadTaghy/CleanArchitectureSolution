using Application.UseCases.UserProfileCase.Query.GetUserList;
using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IUserRepo : IRepositoryBase<Membership_User>
    {
        Task<bool> AnyEntity(Membership_User user);
        Task<Membership_User> FindAsync(int? id, string userName,CancellationToken cancellationToken);
        
    }
}
