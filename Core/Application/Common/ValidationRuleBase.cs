using Application.Common.Interfaces;
using Common;
using Domain;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace Application
{
    public abstract class ValidationRuleBase<T> : AbstractValidator<T>, IValidationRuleBase<T> 
        where T : class, new()
    {
        public ValidationRuleBase()
        {
            AddCheckValidation();
        }
        public virtual void AddCheckValidation()
        {
            Type t = typeof(T);
            foreach (PropertyInfo param in t.GetProperties())
            {
                
                var column = param.GetCustomAttributes().OfType<Column>().FirstOrDefault();
                if (column != null)
                {
                    if (column.IsRequired)
                    {

                        RuleFor(p => param.Name).NotNull()
                            .WithMessage(string.Format(CommonMessage.RequiredMessage, column.Title));
                        RuleFor(p => param.Name).NotEmpty()
                            .WithMessage(string.Format(CommonMessage.NotEmpty, column.Title));
                    }
                    if(column.MaxLength!=int.MaxValue)
                        RuleFor(p => param.Name).MaximumLength(column.MaxLength);
                    if(column.MinLength!=int.MinValue)
                        RuleFor(p=>param.Name).MinimumLength(column.MinLength);
                    if(column.Length!=int.MaxValue)
                        RuleFor(p=>param.Name).Length(column.Length);
                }
            }
        }
    }
}
