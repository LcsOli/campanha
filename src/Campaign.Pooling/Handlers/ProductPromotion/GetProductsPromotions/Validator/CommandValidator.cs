using FluentValidation;
using Campaign.Shared.FluentValidator;
using Campaign.Pooling.Commands.Promotions;

namespace Campaign.Pooling.Handlers.ProductPromotion.GetProductsPromotions.Validator
{
    public class CommandValidator : FluentValidator<GetProductsPromotionsCommand>
    {
        public CommandValidator()
        {
            RuleFor(p => p.PromotionCode)
                .GreaterThan(0)
                .WithMessage("Código promocional deve ser definido");
        }
    }
}
