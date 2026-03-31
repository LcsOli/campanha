using Campaign.Pooling.Commands.ProductPromotions.Get;
using Campaign.Pooling.Repositories.ProductPromotion.ReadOnly;
using Entity = Campaign.Shared.DataBaseContext.Entities.Product;
using Campaign.Pooling.Handlers.ProductPromotion.GetProductsPromotions.Validator;

namespace Campaign.Pooling.Handlers.ProductPromotion.GetProductsPromotions
{
    public class GetProductsPromotionsHandler : IGetProductsPromotionsHandler
    {
        private readonly IProductPromotionReadOnlyRepository _productPromotionReadOnlyRepository;
        public GetProductsPromotionsHandler(IProductPromotionReadOnlyRepository productPromotionReadOnlyRepository)
        {
            _productPromotionReadOnlyRepository = productPromotionReadOnlyRepository;
        }

        public async Task<List<Entity.ProductPromotionSummary>> Handle(GetProductsPromotionsCommand cmd)
        {
            new CommandValidator().Validate(cmd);

            var productPromotions = await _productPromotionReadOnlyRepository.GetByProductsIdsAndPromotionCode(cmd.ProdutcsIds, cmd.PromotionCode);

            new FindedProductsPromotionsValidator()
                .Validate(productPromotions);

            return productPromotions;
        }
    }
}
