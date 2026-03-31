using Campaign.Pooling.Commands.Orders.Get;
using Campaign.Pooling.Commands.ProductPromotions.Get;
using Campaign.Pooling.Handlers.OrderDetail.GetOrdersDetail;
using Campaign.Pooling.Handlers.SellerScore.GetSellersScore;
using Campaign.Pooling.Handlers.ProductPromotion.GetProductsPromotions;

namespace Campaign.Pooling.Orchestrators.UpdateSellerScore
{
    public class CalcuateScoreByProductOrchestrator : ICalcuateScoreByProductOrchestrator
    {
        private readonly IGetSellerScoreHandler _getSellerScoreHandler;
        private readonly IGetOrdersDetailHandler _getOrdersDetailHandler;
        private readonly IGetProductsPromotionsHandler _getProductsPromotionsHandler;
        public CalcuateScoreByProductOrchestrator(IGetSellerScoreHandler getSellerScoreHandler,
                                                  IGetOrdersDetailHandler getOrdersDetailHandler,
                                                  IGetProductsPromotionsHandler getProductsPromotionsHandler)
        {
            _getSellerScoreHandler = getSellerScoreHandler;
            _getOrdersDetailHandler = getOrdersDetailHandler;
            _getProductsPromotionsHandler = getProductsPromotionsHandler;
        }

        public async Task Execute(int promotionCode, DateTime initIn, DateTime endIn)
        {
            var productsPromotions = await _getProductsPromotionsHandler.Handle(new GetProductsPromotionsCommand(promotionCode));

            var ordersDetail = await _getOrdersDetailHandler.Handle(new GetOrdersDetailCommand([.. productsPromotions.Select(p => p.ProductId)], initIn, endIn));

            var sellersScore = await _getSellerScoreHandler.Handle();

            sellersScore.ForEach(sellerScore =>
            {
                var ordersByClient = ordersDetail.Where(o => o.SellerId == sellerScore.SellerId)
                                                 .GroupBy(o => o.CustomerId)
                                                 .Select(o =>
                                                 {
                                                     return new
                                                     {
                                                         CustomerId = o.Key,
                                                         Orders = o.DistinctBy(p => p.ProductId).ToList()
                                                     };
                                                 }).ToList();

                ordersByClient.ForEach(o =>
                {
                    var productsIds = o.Orders.Select(p => p.ProductId);

                    var points = productsPromotions.Where(p => productsIds.Contains(p.ProductId))
                                                   .Sum(p => p.QuantityPointsGoals);

                    sellerScore.UpdateScore(points!.Value);
                });
            });
            _sellerScoreWriteOnlyRepository.Update(sellersScore);
        }
    }
}
