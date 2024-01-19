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
        private readonly ICacheManager _cacheManager;

        public PermissionRepo(PersistanceDBContext context, 
             ICurrentUserSession currentUserSession, ICacheManager cacheManager) :
            base(context, currentUserSession)
        {
            _cacheManager = cacheManager;
        }
        


    }
}
