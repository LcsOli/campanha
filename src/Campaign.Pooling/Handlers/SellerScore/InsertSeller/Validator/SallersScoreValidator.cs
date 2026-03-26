using FluentValidation;
using Campaign.Shared.FluentValidator;
using Campaign.Pooling.Commands.Seller.Create;

namespace Campaign.Pooling.Handlers.Seller.InsertSeller.Validator
{
    public class SallersScoreValidator : FluentValidator<SellersScoreToCreateCommand>
    {
        public SallersScoreValidator()
        {
            RuleFor(s => s.SellerId)
                 .GreaterThan(0)
                 .WithMessage("Identificador do vendedor é necessário.");

            RuleFor(s => s.Name)
                  .NotEmpty()
                  .NotNull()
                  .WithMessage("Nome do gerente do vendedor é necessário.");

            RuleFor(s => s.ManagerName)
                 .NotEmpty()
                 .NotNull()
                 .WithMessage("Nome do vendedor é necessário.");
        }
    }
}
