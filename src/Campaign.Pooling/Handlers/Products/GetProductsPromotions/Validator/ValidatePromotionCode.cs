using FluentValidation;
using Campaign.Shared.FluentValidator;
using Campaign.Pooling.Commands.Promotions;

namespace Campaign.Pooling.Handlers.Products.GetProductsPromotions.Validator
{
    public class ValidatePromotionCode : FluentValidator<GetProductsPromotionsCommand>
    {
        public ValidatePromotionCode()
        {
            RuleFor(p => p.PromotionCode)
                .LessThanOrEqualTo(0)
                .WithMessage("Código promocional deve ser definido");
        }
    }
}
