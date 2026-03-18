using System.Net;
using FluentValidation;
using FluentValidation.Results;
using Campaign.API.Commands.User.Create;
using Campaign.API.Configuration.Exceptions;

namespace Campaign.API.Handlers.RegisterUser.Validator
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {
            RuleFor(r => r.Role)
                .IsInEnum()
                .WithMessage("Role inválida.");

            RuleFor(r => r.TeamId)
                .Empty()
                .WithMessage("Equipe deve ser definida.");

            RuleFor(r => r.Name)
                .Empty()
                .Null()
                .WithMessage("Nome do usuário deve ser definido.")
                .MaximumLength(100)
                .WithMessage("Tamanho máximo do nome do usuário deve ser de 100 caracteres.");

            RuleFor(r => r.Document)
                .Empty()
                .Null()
                .WithMessage("Documento do usuário deve ser definido.")
                .MaximumLength(14)
                .WithMessage("Tamanho máximo do documento deve ser de 14 caracteres.");

            RuleFor(r => r.Password)
                .Empty()
                .Null()
                .WithMessage("Senha do usuário deve ser definido.")
                .MaximumLength(50)
                .WithMessage("Tamanho máximo da senha deve ser de 50 caracteres.");
        }

        public override ValidationResult Validate(ValidationContext<RegisterUserCommand> context)
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
