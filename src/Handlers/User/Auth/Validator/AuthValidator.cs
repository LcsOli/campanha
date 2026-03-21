using FluentValidation;
using Campaign.API.Commands.User.Auth;
using Campaign.API.Configuration.Validator;

namespace Campaign.API.Handlers.User.Auth.Validator
{
    public class AuthValidator : FluentValidator<AuthCommand>
    {
        public AuthValidator()
        {
            RuleFor(a => a.Document)
                .NotNull()
                .NotEmpty()
                .WithMessage("Documento deve ser definido.");

            RuleFor(a => a.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("senha deve ser definida.");
        }
    }
}
