using Campaign.Pooling.Commands.ProductPromotions.Get;
using Campaign.Pooling.Repositories.ProductPromotion.ReadOnly;
using Campaign.Pooling.Handlers.ProductPromotion.ProductPromotionExists.Validator;

namespace Campaign.Pooling.Handlers.ProductPromotion.ProductPromotionExists
{
    public class ProductPromotionExistsHandler : IProductPromotionExistsHandler
    {
        private readonly IProductPromotionReadOnlyRepository _productPromotionReadOnlyRepository;
        public ProductPromotionExistsHandler(IProductPromotionReadOnlyRepository productPromotionReadOnlyRepository)
        {
            _productPromotionReadOnlyRepository = productPromotionReadOnlyRepository;
        }

        public async Task Handle(ProductPromotionExistsCommand cmd)
        {
            new CommandValidator().Validate(cmd);

            var promotionExist = await _productPromotionReadOnlyRepository.Exists(cmd.PromotionCode);

            ProductPromotionExistsValidator
                .Validate(promotionExist);
        }
    }
}
