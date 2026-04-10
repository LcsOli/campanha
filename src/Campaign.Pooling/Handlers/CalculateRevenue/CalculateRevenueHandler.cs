using Campaign.Pooling.Commands.Calculate;
using Campaign.Pooling.Repositories.OrderDetail.ReadOnly;
using Campaign.Pooling.Repositories.ProductPromotionSummary.ReadOnly;
using Campaign.Pooling.Repositories.ProductPromotionReadDataHistory.ReadOnly;

namespace Campaign.Pooling.Handlers.CalculateRevenueTarget
{
    public class CalculateRevenueHandler : ICalculateRevenueHandler
    {
        private readonly IOrderDetailReadOnlyRepository _orderDetailReadOnlyRepository;
        private readonly IProductPromotionSummaryReadOnlyRepository _productPromotionSummaryReadOnlyRepository;
        private readonly IProductPromotionReadDataHistoryRepositorie _productPromotionReadDataHistoryRepositorie;
        public CalculateRevenueHandler(IOrderDetailReadOnlyRepository orderDetailReadOnlyRepository,
                                       IProductPromotionSummaryReadOnlyRepository productPromotionSummaryReadOnlyRepository,
                                       IProductPromotionReadDataHistoryRepositorie productPromotionReadDataHistoryRepositorie)
        {
            _orderDetailReadOnlyRepository = orderDetailReadOnlyRepository;
            _productPromotionSummaryReadOnlyRepository = productPromotionSummaryReadOnlyRepository;
            _productPromotionReadDataHistoryRepositorie = productPromotionReadDataHistoryRepositorie;
        }

        public async Task Handle(CalculateRevenueCommand cmd)
        {
            var currentRevenue = await _orderDetailReadOnlyRepository.CalculateCurrentRevenue(cmd.PromotionCode);

            cmd.SellersScore.ForEach(sellerScore =>
            {
                var revenueSeller = currentRevenue?.FirstOrDefault(revenue => revenue.SellerId == sellerScore.SellerId);
                sellerScore?.UpdateCurrentRevenue(revenueSeller!.Revenue);
            });
        }

        public async Task Handle(CalculateRevenueMonthCommand cmd)
        {
            var lastPromotionRead = await _productPromotionReadDataHistoryRepositorie.GetLast();

            if (lastPromotionRead == null)
                return;

            var productsPromotionsSummariesDates = await _productPromotionSummaryReadOnlyRepository.GetProductPromotionSummariesDates(cmd.PromotionCode);

            var isNewMonth = productsPromotionsSummariesDates!.PreviousPromotionDtInit.Month < productsPromotionsSummariesDates.CurrentPromotionDtInit.Month;
            var isLastMonthOfCampaign = productsPromotionsSummariesDates.CurrentPromotionDtEnd >= productsPromotionsSummariesDates.LastPromotionDtEnd;

            if (!isNewMonth && !isLastMonthOfCampaign)
                return;

            var year = productsPromotionsSummariesDates.PreviousPromotionDtInit.Year;
            var month = productsPromotionsSummariesDates.PreviousPromotionDtInit.Month;

            var lastDayOfMonth = DateTime.DaysInMonth(year, month);

            var revenueByMonth = await _orderDetailReadOnlyRepository.CalculateRevenueByMonth(new DateTime(year, month, 01), new DateTime(year, month, lastDayOfMonth));

            cmd.SellersScore.ForEach(sellerScore =>
            {
                var revenueByMonthSeller = revenueByMonth?.FirstOrDefault(revenue => revenue.SellerId == sellerScore.SellerId);
                sellerScore?.UpdateRevenueByMonth(revenueByMonthSeller!.Revenue, month);

                if (revenueByMonthSeller!.Revenue >= sellerScore!.RevenueTarget)
                    sellerScore.UpdateCoupons((short)(sellerScore.Coupons + 1));
            });
        }
    }
}