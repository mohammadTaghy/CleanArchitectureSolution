﻿using Application.Common.Interfaces;
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
        IUserRepo UserRepo { get; set; }
    }
}