using Campaign.Pooling.Commands.Orders.Get;
using Campaign.Pooling.Commands.ProductPromotions.Get;
using Campaign.Pooling.Handlers.OrderDetail.GetOrdersDetail;
using Campaign.Pooling.Handlers.ProductPromotion.GetProductsPromotions;

namespace Campaign.Pooling.Orchestrators.UpdateSellerScore
{
    public class CalcuateScoreByProductOrchestrator : ICalcuateScoreByProductOrchestrator
    {
        private readonly IGetOrdersDetailHandler _getOrdersDetailHandler;
        private readonly IGetProductsPromotionsHandler _getProductsPromotionsHandler;
        public CalcuateScoreByProductOrchestrator(IGetOrdersDetailHandler getOrdersDetailHandler,
                                                  IGetProductsPromotionsHandler getProductsPromotionsHandler)
        {
            _getOrdersDetailHandler = getOrdersDetailHandler;
            _getProductsPromotionsHandler = getProductsPromotionsHandler;
        }

        public async Task Execute(int promotionCode, DateTime initIn, DateTime endIn)
        {
            var productsPromotion = await _getProductsPromotionsHandler.Handle(new GetProductsPromotionsCommand(promotionCode));

            var ordersDetail = await _getOrdersDetailHandler.Handle(new GetOrdersDetailCommand([.. productsPromotion.Select(p => p.ProductId)], initIn, endIn));
        }
    }
}
