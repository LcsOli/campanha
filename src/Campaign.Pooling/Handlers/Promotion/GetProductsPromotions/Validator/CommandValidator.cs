using FluentValidation;
using Campaign.Shared.FluentValidator;
using Campaign.Pooling.Commands.ProductPromotions.Get;

namespace Campaign.Pooling.Handlers.Promotion.GetProductsPromotions.Validator
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
