using Common;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Common.LessonsCategories.Command.Create
{
    public class CreateLessonsCategoriesCommandValidator : AbstractValidator<CreateLessonsCategoriesCommand>
    {
        public CreateLessonsCategoriesCommandValidator()
        {
            RuleFor(p=>p.Title).MinimumLength(3).MaximumLength(256).WithMessage(string.Format(CommonMessage.MinimumLength,"عنوان"));
            RuleFor(p => p.CategoryType).NotNull().WithMessage(string.Format(CommonMessage.NullException, "نوع"));
            RuleFor(p => p.Name).MinimumLength(3).MaximumLength(256).WithMessage(string.Format(CommonMessage.MinimumLength, "نام"));

        }
    }
}
