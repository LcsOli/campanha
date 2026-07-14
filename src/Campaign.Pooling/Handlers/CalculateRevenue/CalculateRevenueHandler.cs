using Campaign.Pooling.Services;
using Campaign.Pooling.Commands.Calculate;
using Campaign.Pooling.Repositories.Period.ReadOnly;
using Campaign.Pooling.Repositories.Order.ReadOnly;
using Campaign.Pooling.Repositories.ProductPromotionSummary.ReadOnly;

namespace Campaign.Pooling.Handlers.CalculateRevenueTarget
{
    public class CalculateRevenueHandler : ICalculateRevenueHandler
    {
        private readonly IPeriodReadOnlyRepository _periodReadOnlyRepository;
        private readonly IOrderDetailReadOnlyRepository _orderDetailReadOnlyRepository;
        private readonly IProductPromotionSummaryReadOnlyRepository _productPromotionSummaryReadOnlyRepository;
        public CalculateRevenueHandler(IPeriodReadOnlyRepository periodReadOnlyRepository,
                                       IOrderDetailReadOnlyRepository orderDetailReadOnlyRepository,
                                       IProductPromotionSummaryReadOnlyRepository productPromotionSummaryReadOnlyRepository)
        {
            _periodReadOnlyRepository = periodReadOnlyRepository;
            _orderDetailReadOnlyRepository = orderDetailReadOnlyRepository;
            _productPromotionSummaryReadOnlyRepository = productPromotionSummaryReadOnlyRepository;
        }

        public async Task Handle(CalculateRevenueCommand cmd)
        {
            var promotionsDates = await _productPromotionSummaryReadOnlyRepository.GetProductPromotionSummariesDates(cmd.PromotionCode);
            var periods = await _periodReadOnlyRepository.GetByYear(promotionsDates!.CurrentDtEnd.Year);

            var periodService = new PeriodService(periods);

            if (periodService.IsEndOfPeriod(promotionsDates!.CurrentDtEnd))
            {
                cmd.SellersScore.ForEach(sellerScore => sellerScore.ClearCurrentRevenue());
                return;
            }

            var period = periodService.GetPeriod(promotionsDates!.CurrentDtEnd);

            var currentRevenue = await _orderDetailReadOnlyRepository.CalculateRevenueByMonth(period.InitIn, promotionsDates.CurrentDtEnd);

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
            var periods = await _periodReadOnlyRepository.GetByYear(promotionsDates!.CurrentDtEnd.Year);

            var periodService = new PeriodService(periods);

            if (!periodService.IsEndOfPeriod(promotionsDates!.CurrentDtEnd))
                return;

            var period = periodService.GetPeriod(promotionsDates!.CurrentDtEnd);

            var revenueByMonth = await _orderDetailReadOnlyRepository.CalculateRevenueByMonth(period.InitIn, period.EndIn);

            cmd.SellersScore.ForEach(sellerScore =>
            {
                var revenueByMonthSeller = revenueByMonth?.FirstOrDefault(revenue => revenue.SellerId == sellerScore.SellerId);

                if (revenueByMonthSeller == null)
                    return;

                sellerScore.UpdateRevenueByMonth(revenueByMonthSeller!.Revenue);
            });
        }
    }
}