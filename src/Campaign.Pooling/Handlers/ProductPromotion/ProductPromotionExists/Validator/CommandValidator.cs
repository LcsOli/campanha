using FluentValidation;
using Campaign.Shared.FluentValidator;
using Campaign.Pooling.Commands.ProductPromotions.Get;

namespace Campaign.Pooling.Handlers.ProductPromotion.ProductPromotionExists.Validator
{
    public class CommandValidator : FluentValidator<ProductPromotionExistsCommand>
    {
        public CommandValidator()
        {
            RuleFor(p => p.PromotionCode)
                .GreaterThan(0)
                .WithMessage("Código promocional deve ser definido");
        }
    }
}
