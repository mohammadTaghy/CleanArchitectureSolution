using Application.Common.Interfaces;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation.UserProfileValidation
{
    public interface IUserProfileValidation : IValidationRuleBase<Membership_UserProfile>, IValidator<Membership_UserProfile>
    {
    }
}
