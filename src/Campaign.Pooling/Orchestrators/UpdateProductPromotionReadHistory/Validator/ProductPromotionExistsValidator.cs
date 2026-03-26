using FluentValidation;
using Campaign.Shared.FluentValidator;
using Entity = Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.Pooling.Orchestrators.UpdateProductPromotionReadHistory.Validator
{
    public class ProductPromotionExistsValidator : FluentValidator<Entity.ProductPromotionReadDataHistory>
    {
        public ProductPromotionExistsValidator()
        {
            RuleFor(productPromotionReadDataHistory => productPromotionReadDataHistory)
                .Null()
                .WithMessage(p => $"A promoção {p.PromotionCode} foi processada em: {p.ReadAt:dd/MM/YYYY}");
        }
    }
}
