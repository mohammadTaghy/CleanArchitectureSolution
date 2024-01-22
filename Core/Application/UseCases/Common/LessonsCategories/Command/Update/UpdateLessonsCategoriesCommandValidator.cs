using Common;
using FluentValidation;

namespace Application.UseCases.Common.LessonsCategories.Command.Update
{
    public class UpdateLessonsCategoriesCommandValidator : AbstractValidator<UpdateLessonsCategoriesCommand>
    {
        public UpdateLessonsCategoriesCommandValidator()
        {
            RuleFor(p=>p.Title).MinimumLength(3).MaximumLength(256).WithMessage(string.Format(CommonMessage.MinimumLength,"عنوان"));
            RuleFor(p => p.CategoryType).NotNull().WithMessage(string.Format(CommonMessage.NullException, "نوع"));
            RuleFor(p => p.Name).MinimumLength(3).MaximumLength(256).WithMessage(string.Format(CommonMessage.MinimumLength, "نام"));

        }
    }
}
