using FluentValidation;
using Campaign.Shared.FluentValidator;
using Product = Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.Pooling.Handlers.ProductPromotion.GetProductsPromotions.Validator
{
    public class FindedProductsPromotionsValidator : FluentValidator<List<Product.ProductPromotionSummary>>
    {
        public FindedProductsPromotionsValidator()
        {
            RuleFor(p => p.Count)
                .GreaterThan(0)
                .WithMessage("contrados produtos com este código de promoção.");
        }
    }
}
