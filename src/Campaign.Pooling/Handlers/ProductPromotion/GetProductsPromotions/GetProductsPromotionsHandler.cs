using Campaign.Pooling.Commands.Promotions;
using Entity = Campaign.Shared.DataBaseContext.Entities.Product;
using Campaign.Pooling.Handlers.ProductPromotion.GetProductsPromotions.Validator;
using Campaign.Pooling.Repositories.ProductPromotion.ReadOnly;

namespace Campaign.Pooling.Handlers.ProductPromotion.GetProductsPromotions
{
    public class GetProductsPromotionsHandler : IGetProductsPromotionsHandler
    {
        private readonly IProductPromotionReadOnlyRepository _productPromotionReadOnlyRepository;
        public GetProductsPromotionsHandler(IProductPromotionReadOnlyRepository productPromotionReadOnlyRepository)
        {
            _productPromotionReadOnlyRepository = productPromotionReadOnlyRepository;
        }

        public async Task<List<Entity.ProductPromotion>> Handle(GetProductsPromotionsCommand cmd)
        {
            new CommandValidator().Validate(cmd);

            var promotions = await _productPromotionReadOnlyRepository.GetByPromotionCode(cmd.PromotionCode);

            new FindedProductsPromotionsValidator().Validate(promotions);

            return promotions;
        }
    }
}
