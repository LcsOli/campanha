using Campaign.Pooling.Commands.Promotions;
using Campaign.Shared.DataBaseContext.Entities.Product;
using Campaign.Pooling.Repositories.Products.ProductPromotion.ReadOnly;
using Campaign.Pooling.Handlers.Products.GetProductsPromotions.Validator;

namespace Campaign.Pooling.Handlers.Products.GetPromotionsProducts
{
    public class GetProductsPromotionsHandler : IGetProductsPromotionsHandler
    {
        private readonly IProductPromotionReadOnlyRepository _productPromotionReadOnlyRepository;
        public GetProductsPromotionsHandler(IProductPromotionReadOnlyRepository productPromotionReadOnlyRepository)
        {
            _productPromotionReadOnlyRepository = productPromotionReadOnlyRepository;
        }

        public async Task<List<ProductPromotion>> Handle(GetProductsPromotionsCommand cmd)
        {
            new ValidatePromotionCode().Validate(cmd);

            var promotions = await _productPromotionReadOnlyRepository.GetByPromotionCode(cmd.PromotionCode);

            new ValidateFindedProductsPromotions().Validate(promotions);

            return promotions;
        }
    }
}
