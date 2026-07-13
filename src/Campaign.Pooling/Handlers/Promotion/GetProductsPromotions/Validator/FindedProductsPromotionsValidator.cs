using FluentValidation;
using Campaign.Shared.FluentValidator;
using Campaign.Shared.DTOs.Response.Product;

namespace Campaign.Pooling.Handlers.Promotion.GetProductsPromotions.Validator
{
    public class FindedProductsPromotionsValidator : FluentValidator<List<ProductPromotionResponse>>
    {
        public FindedProductsPromotionsValidator()
        {
            RuleFor(p => p.Count)
                .GreaterThan(0)
                .WithMessage("contrados produtos com este código de promoção.");
        }
    }
}
