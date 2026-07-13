using System.Net;
using Campaign.Shared.Exceptions;
using Entity = Campaign.Shared.DataBaseContext.Entities.Product;
using Campaign.Pooling.Commands.ProductPromotionReadHistory.Get;
using Campaign.Pooling.Repositories.ProductPromotionReadDataHistory.ReadOnly;

namespace Campaign.Pooling.Handlers.PromotionHistory.GetLastProductPromotionsHistory
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
            var productPromotionReadDataHistory = await Handle(new GetProductPromotionReadHistoryCommand(cmd.promotionCode));

            if (productPromotionReadDataHistory != null)
                throw new CompaignException(HttpStatusCode.Forbidden, $"A promoção {productPromotionReadDataHistory.PromotionCode} foi processada em: {productPromotionReadDataHistory.ReadAt:dd/MM/yyyy}");
        }

        public async Task<Entity.ProductPromotionReadDataHistory?> Handle(GetProductPromotionReadHistoryCommand cmd)
        {
            return await _productPromotionReadDataHistoryRepositorie.Get(cmd.promotionCode);
        }
    }
}