using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IValidationRuleBase<T> where T : class, IEntity
    {
        void AddCheckValidation();
    }
}
