using FluentValidation;
using Campaign.Shared.FluentValidator;
using Campaign.Pooling.Commands.ProductPromotions.Get;

namespace Campaign.Pooling.Handlers.Promotion.ProductPromotionExists.Validator
{
    public class ProductPromotionExistsDataValidator : FluentValidator<ProductPromotionExistsCommand>
    {
        public ProductPromotionExistsDataValidator()
        {
            RuleFor(p => p.PromotionCode)
                .GreaterThan(0)
                .WithMessage("Código promocional deve ser definido");
        }
    }
}
