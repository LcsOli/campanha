using Campaign.Pooling.Commands.ProductPromotionReadHistory.Get;
using Entity = Campaign.Shared.DataBaseContext.Entities.Product;
using Campaign.Pooling.Repositories.ProductPromotionReadDataHistory.ReadOnly;
using Campaign.Pooling.Handlers.ProductPromotionReadDataHistory.GetProductPromotionReadDataHistory.Validator;

namespace Campaign.Pooling.Handlers.ProductPromotionHistory.GetLastProductPromotionsHistory
{
    public class GetProductPromotionReadDataHistoryHandler : IGetProductPromotionReadDataHistoryHandler
    {
        private readonly IProductPromotionReadDataHistoryRepositorie _productPromotionReadDataHistoryRepositorie;
        public GetProductPromotionReadDataHistoryHandler(IProductPromotionReadDataHistoryRepositorie productPromotionReadDataHistoryRepositorie)
        {
            _productPromotionReadDataHistoryRepositorie = productPromotionReadDataHistoryRepositorie;
        }

        public async Task Handle(ProductPromotionWasReadCommand cmd)
        {
            var command = await Handle(new GetProductPromotionReadHistoryCommand(cmd.promotionCode));
            ProductPromotionWasReadValidator.Validate(command!);
        }

        public async Task<Entity.ProductPromotionReadDataHistory?> Handle(GetProductPromotionReadHistoryCommand cmd)
        {
            return await _productPromotionReadDataHistoryRepositorie.Get(cmd.promotionCode);
        }
    }
}