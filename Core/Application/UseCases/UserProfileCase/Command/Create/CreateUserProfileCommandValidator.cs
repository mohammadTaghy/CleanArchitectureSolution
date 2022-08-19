using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UserProfileCase.Command.Create
{
    public class CreateUserProfileCommandValidator : AbstractValidator<CreateUserProfileCommand>
    {
        public CreateUserProfileCommandValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.FirstName).MaximumLength(512).NotEmpty();
            RuleFor(x => x.LastName).MaximumLength(512).NotEmpty();
            RuleFor(x => x.MobileNumber).MaximumLength(11).NotEmpty();
            RuleFor(x => x.NationalCode).MaximumLength(10);
        }
    }
}
