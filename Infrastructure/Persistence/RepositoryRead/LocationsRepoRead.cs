using Application;
using Application.IRepository;
using Common;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Persistence.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    internal class LocationsRepoRead : HierarchyEntityRepoRead<Membership_Locations>, ILocationsRepoRead
    {
        public LocationsRepoRead(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
