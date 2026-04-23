using Campaign.Pooling.Services;
using Campaign.Pooling.Commands.Calculate;
using Campaign.Pooling.Repositories.OrderDetail.ReadOnly;
using Campaign.Pooling.Repositories.ProductPromotionSummary.ReadOnly;

namespace Campaign.Pooling.Handlers.CalculateRevenueTarget
{
    public class CalculateRevenueHandler : ICalculateRevenueHandler
    {
        private readonly IOrderDetailReadOnlyRepository _orderDetailReadOnlyRepository;
        private readonly IProductPromotionSummaryReadOnlyRepository _productPromotionSummaryReadOnlyRepository;
        public CalculateRevenueHandler(IOrderDetailReadOnlyRepository orderDetailReadOnlyRepository,
                                       IProductPromotionSummaryReadOnlyRepository productPromotionSummaryReadOnlyRepository)
        {
            _orderDetailReadOnlyRepository = orderDetailReadOnlyRepository;
            _productPromotionSummaryReadOnlyRepository = productPromotionSummaryReadOnlyRepository;
        }

        public async Task Handle(CalculateRevenueCommand cmd)
        {
            var promotionsDates = await _productPromotionSummaryReadOnlyRepository.GetProductPromotionSummariesDates(cmd.PromotionCode);

            if (ProcessRevenueService.IsEndOfPeriod(promotionsDates!.CurrentDtEnd))
            {
                cmd.SellersScore.ForEach(sellerScore => sellerScore.ClearCurrentRevenue());
                return;
            }

            var period = ProcessRevenueService.GetPeriod(promotionsDates!.CurrentDtEnd);

            var currentRevenue = await _orderDetailReadOnlyRepository.CalculateRevenueByMonth(period.Init, promotionsDates.CurrentDtEnd);

            cmd.SellersScore.ForEach(sellerScore =>
            {
                var revenueSeller = currentRevenue?.FirstOrDefault(revenue => revenue.SellerId == sellerScore.SellerId);

                if (revenueSeller == null)
                    return;

                sellerScore.UpdateCurrentRevenue(revenueSeller!.Revenue);
            });
        }

        public async Task Handle(CalculateRevenueMonthCommand cmd)
        {
            var promotionsDates = await _productPromotionSummaryReadOnlyRepository.GetProductPromotionSummariesDates(cmd.PromotionCode);

            if (!ProcessRevenueService.IsEndOfPeriod(promotionsDates!.CurrentDtEnd))
                return;

            var month = promotionsDates.PreviousDtInit.Month;

            var period = ProcessRevenueService.GetPeriod(promotionsDates!.CurrentDtEnd);

            var revenueByMonth = await _orderDetailReadOnlyRepository.CalculateRevenueByMonth(period.Init, period.end);

            cmd.SellersScore.ForEach(sellerScore =>
            {
                var revenueByMonthSeller = revenueByMonth?.FirstOrDefault(revenue => revenue.SellerId == sellerScore.SellerId);

                if (revenueByMonthSeller == null)
                    return;

                sellerScore.UpdateRevenueByMonth(revenueByMonthSeller!.Revenue, month);
            });
        }
    }
}