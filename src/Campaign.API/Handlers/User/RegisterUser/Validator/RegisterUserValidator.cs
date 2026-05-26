using FluentValidation;
using Campaign.Shared.FluentValidator;
using Campaign.API.Commands.User.Create;

namespace Campaign.API.Handlers.User.RegisterUser.Validator
{
    public class RegisterUserValidator : FluentValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {
            RuleFor(r => r.Role)
                .IsInEnum()
                .WithMessage("Role inválida.");

            RuleFor(r => r.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Nome do usuário deve ser definido.")
                .MaximumLength(100)
                .WithMessage("Tamanho máximo do nome do usuário deve ser de 100 caracteres.");

            RuleFor(r => r.Document)
                .NotEmpty()
                .NotNull()
                .WithMessage("Documento do usuário deve ser definido.")
                .MaximumLength(14)
                .WithMessage("Tamanho máximo do documento deve ser de 14 caracteres.");

            RuleFor(r => r.Password)
                .NotEmpty()
                .NotNull()
                .WithMessage("Senha do usuário deve ser definido.")
                .MaximumLength(50)
                .WithMessage("Tamanho máximo da senha deve ser de 50 caracteres.");
        }
    }
}
