using Application.UseCases.UserCase.Command.Create;
using Application.UseCases.UserCase.Command.Update;
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
        Task Update(UpdateUserCommand command);
        Task Insert(CreateUserCommand command);
        Task<List<int>> GetRolesId(int userId);
    }
}
