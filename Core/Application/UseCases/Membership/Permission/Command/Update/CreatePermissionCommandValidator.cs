using Application.UseCases.Membership.Permission.Command.Update;
using Common;
using FluentValidation;

namespace Application.UseCases.Membership.Permission.Command.Create
{
    public class UpdatePermissionCommandValidator : AbstractValidator<UpdatePermissionCommand>
    {
        public UpdatePermissionCommandValidator()
        {
            RuleFor(p => p.Id).NotNull();
            RuleFor(p => p.IsActive).NotNull();
            RuleFor(p => p.Title).MinimumLength(2).MaximumLength(256).WithMessage(string.Format(CommonMessage.MinimumLength, "عنوان"));
            RuleFor(p => p.FeatureType).NotNull();
            RuleFor(p => p.Name).MinimumLength(2).MaximumLength(256).WithMessage(string.Format(CommonMessage.MinimumLength, "نام"));
        }
    }
}
