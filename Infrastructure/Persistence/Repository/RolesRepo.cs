using Application;
using Application.UseCases;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class RolesRepo : RepositoryBase<Roles>, IRolesRepo
    {
        public RolesRepo(PersistanceDBContext context) : base(context)
        {

        }

        public Task<List<RolesDto>> ItemsAsList(GetRolesQuery request, out int count)
        {
            int index = (request.PageIndex - 1);
            index = index < 0 ? 0 : index;
            var quesy = GetAllAsQueryable().Where(p => p.RoleName.Contains(request.SerchText));
            count=quesy.Count();
            List<RolesDto> resultList = new List<RolesDto>();
            if (count > 0)
                resultList =  quesy.Select(p => new RolesDto
                {
                    IsAdmin=p.IsAdmin,
                    RoleName=p.RoleName
                }).Skip(index*request.PageSize).Take(request.PageSize).ToList();
            return Task.FromResult(resultList);

        }
    }
}
