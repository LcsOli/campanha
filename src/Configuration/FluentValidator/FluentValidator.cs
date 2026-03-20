using System.Net;
using FluentValidation;
using FluentValidation.Results;
using Campaign.API.Configuration.Exceptions;

namespace Campaign.API.Configuration.Validator
{
    public class FluentValidator<T> : AbstractValidator<T> where T : class
    {
        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var result = base.Validate(context);

            if (!result.IsValid)
            {
                var messages = new List<ExceptionMessage>();
                result.Errors.ForEach(error => messages.Add(new(error.ErrorMessage)));

                throw new CompaignExceptions(HttpStatusCode.BadRequest, messages);
            }

            return result;
        }
    }
}
