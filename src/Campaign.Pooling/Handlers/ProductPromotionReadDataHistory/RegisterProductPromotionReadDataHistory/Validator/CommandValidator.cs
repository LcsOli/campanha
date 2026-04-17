using FluentValidation;
using Campaign.Shared.FluentValidator;
using Campaign.Pooling.Commands.ProductPromotionReadHistory.Create;

namespace Campaign.Pooling.Handlers.ProductPromotionReadDataHistory.RegisterNewHistory.Validator
{
    public class CommandValidator : FluentValidator<RegisterProductPromotionReadDataHistoryCommand>
    {
        public CommandValidator()
        {
            RuleFor(r => r.promotionCode)
                .GreaterThan(0)
                .WithMessage("Código da promoção deve ser definido.");
        }
    }
}
