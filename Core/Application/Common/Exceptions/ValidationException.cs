using Common;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public sealed class ValidationException : Exception
    {
        public ValidationException()
            : base(CommonMessage.ValidationMessage)
        {
            Failures = new Dictionary<string, string[]>();
        }
        public ValidationException(ValidationFailure failure)
            : this()
        {
            Failures.Add(failure.PropertyName, new string[] { failure.ErrorMessage });
        }
        public ValidationException(List<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }

        public IDictionary<string, string[]> Failures { get; }
    }
}
