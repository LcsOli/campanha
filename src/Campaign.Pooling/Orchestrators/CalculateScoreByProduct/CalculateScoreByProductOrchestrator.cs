using Campaign.Processor.API.Commands.Summaries.Create;
using Campaign.Pooling.Commands.CalculateScoreByProduct;
using Campaign.Pooling.Handlers.CalculateScoreByProduct;
using Campaign.Pooling.Repositories.OrderDetail.ReadOnly;
using Campaign.Processor.API.Handlers.RegisterSellerScoreProductResume;
using EntitySellerScore = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Processor.API.Orchestrators.CalculateScoreByProduct
{
    public class CalculateScoreByProductOrchestrator : ICalculateScoreByProductOrchestrator
    {
        private readonly IOrderDetailReadOnlyRepository _orderDetailReadOnlyRepository;
        private readonly ICalculateScoreByProductHandler _calculateScoreByProductHandler;
        private readonly IRegisterSellerScoreProductSummariesHandler _registerSellerScoreProductSummariesHandler;
        public CalculateScoreByProductOrchestrator(IOrderDetailReadOnlyRepository orderDetailReadOnlyRepository,
                                                   ICalculateScoreByProductHandler calculateScoreByProductHandler,
                                                   IRegisterSellerScoreProductSummariesHandler registerSellerScoreProductSummariesHandler)
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

            await _registerSellerScoreProductSummariesHandler.Handle(new RegisterSellerScoreProductSummariesCommand(PromotionCode: promotionCode,
                                                                                                                    SellersScores: SellersScores,
                                                                                                                    OrdersDetails: ordersDetails));
        }
    }
}
