using Application;
using Application.UseCases;
using Domain.Entities;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Common;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Persistence.Repository
{
    public class PermissionRepo : HierarchyEntityRepo<Membership_Permission>, IPermissionRepo
    {
        private readonly IUserRolesRepo _userRolesRepo;
        private readonly IRolesPermissionRepo _rolesPermissionRepo;

        public PermissionRepo(PersistanceDBContext context, IUserRolesRepo userRolesRepo, IRolesPermissionRepo rolesPermissionRepo, ICurrentUserSession currentUserSession) :
            base(context, currentUserSession)
        {
            _userRolesRepo = userRolesRepo;
            _rolesPermissionRepo = rolesPermissionRepo;
        }
        #region CustomGet
        public async Task<List<PermissionTreeDto>> GetCurrentRolePermissions(int userId)
        {

            List<Membership_Permission> permissions = await ItemListAdo("sp_CurrentUserPermissions",
                 new SqlParameter[] {
                    new SqlParameter{ParameterName="userId",Value=userId,SqlDbType=SqlDbType.Int}
                 });
            List<PermissionTreeDto> permissionTreeDtos = permissions.Select(p => new PermissionTreeDto
            {
                Id = p.Id,
                Title = p.Title,
                Name = p.Name,
                FeatureType = p.FeatureType,
                ParentId = p.ParentId
            }).ToList();
            List<PermissionTreeDto> parents = permissionTreeDtos.Where(p => p.ParentId == null).ToList();
            ChangeToHierarchy(parents, permissionTreeDtos.Except(parents).ToList());
            return parents;

        }

        public async Task<List<PermissionTreeDto>> GetPermissions(int? roleId)
        {
            List<Membership_Permission> permissions = await ItemListAdo("sp_CurrentRolePermissions",
                new SqlParameter[] {
                    new SqlParameter{ParameterName="roleId",Value=roleId,SqlDbType=SqlDbType.Int}
                });
            List<int> permissionIds = new List<int>();
            if (roleId != null)
                permissionIds = await _rolesPermissionRepo.GetPermissions(roleId);
            List<PermissionTreeDto> permissionTreeDtos = permissions
                   .Select(p => new PermissionTreeDto
                   {
                       Id = p.Id,
                       Title = p.Title,
                       Name = p.Name,
                       FeatureType = p.FeatureType,
                       ParentId = p.ParentId
                   }).ToList();
            permissionTreeDtos.Where(p => permissionIds.Contains(p.Id)).ToList().ForEach(
                p => p.HasPermission = true
                );
            permissionTreeDtos.ForEach(p => p.ChildList = new List<ICommonTreeDto>());
            List<PermissionTreeDto> parents = permissionTreeDtos.Where(p => p.ParentId == null).ToList();
            ChangeToHierarchy<PermissionTreeDto>(parents, permissionTreeDtos.Except(parents).ToList());
            return parents;
        }
        #endregion

        public override Task Insert(Membership_Permission entity)
        {
            if (entity.ParentId != null && entity.ParentId != 0)
            {
                Membership_Permission parentPermission = base.Find(entity.ParentId.Value);
                entity.LevelChar = (char)((int)parentPermission.LevelChar + 1);
                entity.AutoCode = GetMaxAutoCode(entity.ParentId, entity.LevelChar);
                entity.FullKeyCode = parentPermission.FullKeyCode + entity.LevelChar + entity.AutoCode;
            }
            else
            {
                entity.ParentId = null;
                entity.LevelChar = 'a';
                entity.AutoCode = GetMaxAutoCode(entity.ParentId, 'a');
                entity.FullKeyCode = entity.LevelChar + entity.AutoCode.ToString();
            }
            return base.Insert(entity);
        }


    }
}
