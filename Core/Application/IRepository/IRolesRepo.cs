using Application.UseCases;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IRolesRepo : IRepositoryBase<Membership_Roles>
    {
        Task Insert(CreateRoleCommand request);
        Task Update(UpdateRoleCommand request);
    }
}
