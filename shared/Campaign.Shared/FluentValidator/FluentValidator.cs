using System.Net;
using FluentValidation;
using FluentValidation.Results;
using Campaign.Shared.Exceptions;

namespace Campaign.Shared.FluentValidator
{
    public abstract class FluentValidator<T> : AbstractValidator<T> where T : class
    {
        //TODO - Verificar a possibilidade de capturar codigo de erro para retornar na mensagem.

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