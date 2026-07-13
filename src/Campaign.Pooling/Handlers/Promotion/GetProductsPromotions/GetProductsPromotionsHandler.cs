using Campaign.Shared.DTOs.Response.Product;
using Campaign.Pooling.Commands.ProductPromotions.Get;
using Campaign.Pooling.Repositories.ProductPromotion.ReadOnly;
using Campaign.Pooling.Handlers.Promotion.GetProductsPromotions.Validator;

namespace Campaign.Pooling.Handlers.Promotion.GetProductsPromotions
{
    public class GetProductsPromotionsHandler : IGetProductsPromotionsHandler
    {
        private readonly IProductPromotionReadOnlyRepository _productPromotionReadOnlyRepository;
        public GetProductsPromotionsHandler(IProductPromotionReadOnlyRepository productPromotionReadOnlyRepository)
        {
            _productPromotionReadOnlyRepository = productPromotionReadOnlyRepository;
        }

        public async Task<List<ProductPromotionResponse>> Handle(GetProductsPromotionsCommand cmd)
        {
            new CommandValidator().Validate(cmd);

            var productPromotions = await _productPromotionReadOnlyRepository.GetByProductsIdsAndPromotionCode(cmd.PromotionCode, cmd.ProdutcsIds);

            new FindedProductsPromotionsValidator()
                .Validate(productPromotions);

            return productPromotions;
        }
    }
}
