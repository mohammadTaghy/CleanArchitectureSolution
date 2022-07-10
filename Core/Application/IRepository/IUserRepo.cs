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
    public interface IUserRepo : IRepositoryBase<User>
    {
        Task<bool> AnyEntity(User user);
        Task<User> FindAsync(int? id, string userName,CancellationToken cancellationToken);
        
    }
}
