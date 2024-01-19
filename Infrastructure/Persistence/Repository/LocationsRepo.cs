using Application;
using Application.IRepository;
using Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    internal class LocationsRepo : HierarchyEntityRepo<Membership_Locations>, ILocationsRepo
    {
        public LocationsRepo(PersistanceDBContext context, ICurrentUserSession currentUserSession) : base(context, currentUserSession)
        {
        }
    }
}
