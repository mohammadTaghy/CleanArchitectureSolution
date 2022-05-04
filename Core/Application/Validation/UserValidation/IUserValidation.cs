using Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation
{
    public interface IUserValidation:IValidationRuleBase<IUser>, IValidator<IUser>
    {
        IUserRepoRead UserRepo { get; set; }
    }
}
