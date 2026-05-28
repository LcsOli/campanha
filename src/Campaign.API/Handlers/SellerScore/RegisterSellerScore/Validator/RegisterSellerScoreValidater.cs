using FluentValidation;
using Campaign.Shared.FluentValidator;
using Campaign.API.Commands.SellerScore.Create;

namespace Campaign.API.Handlers.SellerScore.RegisterSellerScore.Validator
{
    public class RegisterSellerScoreValidater : FluentValidator<RegisterSellerScoreCommand>
    {
        public RegisterSellerScoreValidater()
        {
            RuleFor(x => x.SellerId)
                .GreaterThan(0)
                .WithMessage("Id do vendedor deve ser definido.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Nome do vendedor deve ser definido.");

            RuleFor(x => x.TeamId)
                .GreaterThan(0)
                .WithMessage("Equipe do vendedor deve ser definido.");

            RuleFor(x => x.SellerManagerId)
                .GreaterThan(0)
                .WithMessage("Id do gerente deve ser definido.");

            RuleFor(x => x.SellerManagerName)
                .NotEmpty()
                .WithMessage("Nome do gerente deve ser definido.");
        }
    }
}
