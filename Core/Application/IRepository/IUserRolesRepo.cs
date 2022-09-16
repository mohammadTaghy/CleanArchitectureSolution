using Application.UseCases;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IUserRolesRepo : IRepositoryBase<UserRoles>
    {
        Task<List<int>> GetRolesId(int userId);
        Task<bool> Insert(CreateUserRolesCommand request);
    }
}
