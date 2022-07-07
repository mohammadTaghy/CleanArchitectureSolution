using Domain;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation
{
    public interface IUserValidation:IValidationRuleBase<User>, IValidator<User>
    {
        IUserRepoRead UserRepo { get; set; }
    }
}
