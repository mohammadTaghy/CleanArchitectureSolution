using Application;
using Application.Common;
using Application.IRepository;
using Common;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
        public LocationsRepoRead(IOptions<MongoDatabaseOption> config, IDirectExchangeRabbitMQ directExchangeRabbitMQ) : base(config, directExchangeRabbitMQ)
        {
        }
    }
}
