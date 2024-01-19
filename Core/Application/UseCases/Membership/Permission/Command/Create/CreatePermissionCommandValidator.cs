using Common;
using Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Membership.Permission.Command.Create
{
    public class CreatePermissionCommandValidator: AbstractValidator<CreatePermissionCommand>
    {
        public CreatePermissionCommandValidator()
        {
            RuleFor(p=>p.IsActive).NotNull();
            RuleFor(p=>p.Title).MinimumLength(2).MaximumLength(256).WithMessage(string.Format(CommonMessage.MinimumLength,"عنوان"));
            RuleFor(p=>p.FeatureType).NotNull();
            RuleFor(p=>p.Name).MinimumLength(2).MaximumLength(256).WithMessage(string.Format(CommonMessage.MinimumLength, "نام"));
        }
    }
}
