using System.Net;
using Campaign.Shared.Exceptions;
using FluentValidation;
using FluentValidation.Results;

namespace Campaign.API.Configuration.Validator
{
    public abstract class FluentValidator<T> : AbstractValidator<T> where T : class
    {
        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var result = base.Validate(context);

            if (!result.IsValid)
            {
                var messages = new List<ExceptionMessage>();
                result.Errors.ForEach(error => messages.Add(new(error.ErrorMessage)));

                throw new CompaignCollectionMessagesExceptions(HttpStatusCode.BadRequest, messages);
            }

            return result;
        }
    }
}
