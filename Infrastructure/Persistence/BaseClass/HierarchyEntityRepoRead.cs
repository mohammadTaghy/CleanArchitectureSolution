using Application.Common;
using Application.Common.Interfaces;
using Common;
using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.BaseClass
{
    public class HierarchyEntityRepoRead<T> : RepositoryReadBase<T>
         where T : class, IHierarchyEntity<T>
    {
        public HierarchyEntityRepoRead(IOptions<MongoDatabaseOption> config, IDirectExchangeRabbitMQ directExchangeRabbitMQ) : base(config, directExchangeRabbitMQ)
        {
        }
        protected void ChangeToHierarchy<E>(List<E> parents, List<E> childs)
            where E : class, ICommonTreeDto<E>, new()
        {
            List<E> nextLevel = new List<E>();
            parents.ForEach(p =>
            {
                p.ChildList = childs.Where(q => q.ParentId == p.Id).ToList();
                p.HasChild = p.ChildList.Any();
                nextLevel.AddRange(p.ChildList.ToList<E>());
            });
            var othersChild = childs.Except(nextLevel).ToList();
            if (othersChild.Any())
                ChangeToHierarchy<E>(nextLevel, othersChild);

        }

    }
}
