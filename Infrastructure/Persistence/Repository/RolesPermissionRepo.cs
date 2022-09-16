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
    public class RolesPermissionRepo : RepositoryBase<RolesPermission>, IRolesPermissionRepo
    {
        private readonly IUserRolesRepo _userRolesRepo;
        public RolesPermissionRepo(PersistanceDBContext context, IUserRolesRepo userRolesRepo) : base(context)
        {
            _userRolesRepo = userRolesRepo;
        }
        #region CustomGet

        public async Task<List<PermissionTreeDto>> GetCurrentRolePermissions(int userId)
        {
            List<int> rolesId = await _userRolesRepo.GetRolesId(userId);
            List<PermissionTreeDto> permissionTreeDtos = GetAllAsQueryable()
                .Where(p => rolesId.Contains(p.RolesId)).
                    Select(p => new PermissionTreeDto
                    {
                        Id = p.PermissionId,
                        Title = p.Permission.Title,
                        Name = p.Permission.Name,
                        ParentId = p.Permission.ParentId
                    }).ToList();
            List<PermissionTreeDto> parents = permissionTreeDtos.Where(p => p.ParentId == null).ToList();
            ChangeToHierarchy(parents, permissionTreeDtos.Except(parents).ToList());
            return parents;

        }

        public async Task<List<PermissionTreeDto>> GetPermissions(int roleId)
        {
            List<int> permissionIds = await GetAllAsQueryable().Where(p=>p.RolesId==roleId).
                Select(p=>p.PermissionId).ToListAsync();
            List<PermissionTreeDto> permissionTreeDtos = GetAllAsQueryable()
                   .Select(p => new PermissionTreeDto
                   {
                       Id = p.PermissionId,
                       Title = p.Permission.Title,
                       Name = p.Permission.Name,
                       ParentId = p.Permission.ParentId
                   }).ToList();
            permissionTreeDtos.Where(p => permissionIds.Contains(p.Id)).ToList().ForEach(
                p => p.HasPermission = true
                );
            List<PermissionTreeDto> parents = permissionTreeDtos.Where(p => p.ParentId == null).ToList();
            ChangeToHierarchy(parents, permissionTreeDtos.Except(parents).ToList());
            return parents;
        }


        #endregion
        #region Manipulate
        public async Task<bool> Insert(CreateRolesPermissionCommand request)
        {
            try
            {
                List<int> oldPermissionIds = await GetAllAsQueryable().Where(p => p.RolesId == request.RolesId).
                Select(p => p.PermissionId).ToListAsync();
                List<int> deletedPermissions = oldPermissionIds.Except(request.PermissionIds).ToList();
                List<int> newPermissions = request.PermissionIds.Except(deletedPermissions).ToList();
                deletedPermissions.ForEach(p => base.Context.Entry(p).State = EntityState.Deleted);
                newPermissions.ForEach(p => new RolesPermission
                {
                    PermissionId = p,
                    RolesId = request.RolesId
                });
                await base.Save();
                return true;

            }
            catch (Exception)
            {
                //log
                return false;
            }
            
        }
        #endregion
        #region PrivateMethod
        private void ChangeToHierarchy(List<PermissionTreeDto> parents, List<PermissionTreeDto> childs)
        {
            List<int> parentIds = new List<int>();
            List<PermissionTreeDto> nextLevel = new List<PermissionTreeDto>();
            parents.ForEach(p =>
            {
                p.ChildList.AddRange(childs.Where(q => q.ParentId == p.Id));
                parentIds.Add(p.Id);
                nextLevel.AddRange(childs.Where(q => q.ParentId == p.Id));
            });
            var othersChild = childs.Except(nextLevel).ToList();
            if (othersChild.Any())
                ChangeToHierarchy(nextLevel, othersChild);

        }
        #endregion
    }
}
