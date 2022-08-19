using Application.Common.Interfaces;
using Common;
using Domain;
using FluentValidation;
using System.Reflection;

namespace Application
{
    public abstract class ValidationRuleBase<T> : AbstractValidator<T>, IValidationRuleBase<T> where T : class, IEntity
    {
        public virtual void AddCheckValidation()
        {
            Type t = typeof(T);
            foreach (PropertyInfo param in t.GetProperties())
            {
                
                var column = param.GetCustomAttributes().OfType<Column>().FirstOrDefault();
                if (column != null && column.IsRequired)
                {

                    RuleFor(p => param.Name).NotNull()
                        .WithMessage(string.Format(CommonMessage.RequiredMessage, column.Title));
                    RuleFor(p => param.Name).NotEmpty()
                        .WithMessage(string.Format(CommonMessage.NotEmpty, column.Title));
                    RuleFor(p => param.Name).MinimumLength(6)
                        .WithMessage(string.Format(CommonMessage.MinimumLength, column.Title));
                }
            }
        }
    }
}
