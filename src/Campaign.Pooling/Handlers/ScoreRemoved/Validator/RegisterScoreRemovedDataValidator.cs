using FluentValidation;
using Campaign.Shared.FluentValidator;
using Campaign.Processor.API.Commands.ScoreRemoved.Create;

namespace Campaign.Processor.API.Handlers.ScoreCanceled.Validator
{
    public class RegisterScoreRemovedDataValidator : FluentValidator<RegisterScoreRemovedCommand>
    {
        public RegisterScoreRemovedDataValidator()
        {
            RuleFor(x => x.OrdersDetails.Count)
                .GreaterThan(0)
                .WithMessage("Lista de vendedores devem ser definidos.");

            RuleFor(c => c.PromotionCode)
                .GreaterThan(0)
                .WithMessage("Detalhes da venda devem ser definidos.");
        }
    }
}
