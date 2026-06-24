using Campaign.Processor.API.Commands.Summaries.Create;
using Campaign.Pooling.Commands.CalculateScoreByProduct;
using Campaign.Pooling.Handlers.CalculateScoreByProduct;
using Campaign.Pooling.Repositories.OrderDetail.ReadOnly;
using Campaign.Processor.API.Handlers.RegisterSellerScoreProductSummary;
using EntitySellerScore = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Processor.API.Orchestrators.CalculateScoreByProduct
{
    public class CalculateScoreByProductOrchestrator : ICalculateScoreByProductOrchestrator
    {
        private readonly IOrderDetailReadOnlyRepository _orderDetailReadOnlyRepository;
        private readonly ICalculateScoreByProductHandler _calculateScoreByProductHandler;
        private readonly IRegisterSellerScoreProductSummaryHandler _registerSellerScoreProductSummariesHandler;
        public CalculateScoreByProductOrchestrator(IOrderDetailReadOnlyRepository orderDetailReadOnlyRepository,
                                                   ICalculateScoreByProductHandler calculateScoreByProductHandler,
                                                   IRegisterSellerScoreProductSummaryHandler registerSellerScoreProductSummariesHandler)
        {
            _orderDetailReadOnlyRepository = orderDetailReadOnlyRepository;
            _calculateScoreByProductHandler = calculateScoreByProductHandler;
            _registerSellerScoreProductSummariesHandler = registerSellerScoreProductSummariesHandler;
        }

        public async Task Execute(int promotionCode, List<EntitySellerScore.SellerScore> SellersScores)
        {
            var ordersDetails = await _orderDetailReadOnlyRepository.GetByPromotionCode(promotionCode);

            _calculateScoreByProductHandler.Handle(new CalculateScoreByProductCommand(PromotionCode: promotionCode,
                                                                                      SellersScores: SellersScores,
                                                                                      OrdersDetails: ordersDetails));

            await _registerSellerScoreProductSummariesHandler.Handle(new RegisterSellerScoreProductSummaryCommand(PromotionCode: promotionCode,
                                                                                                                    SellersScores: SellersScores,
                                                                                                                    OrdersDetails: ordersDetails));
        }
    }
}
