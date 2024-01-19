using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IValidationRuleBase<T> where T : class, new()
    {
        void AddCheckValidation();
    }
}
