using Campaign.Pooling.Commands.Calculate;
using Campaign.Pooling.Repositories.OrderDetail.ReadOnly;
using Campaign.Pooling.Handlers.CalculateRevenue.Validator;
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
            var promotionsDates = await _productPromotionSummaryReadOnlyRepository.GetProductPromotionSummariesDates(cmd.PromotionCode);

            if (promotionsDates!.PreviousDtInit.Month < promotionsDates.CurrentDtInit.Month)
                cmd.SellersScore.ForEach(sellerScore => sellerScore.ClearCurrentRevenue());

            var year = promotionsDates!.CurrentDtInit.Year;

            var monthOfDtInitIsLessOfDtEnd = promotionsDates.CurrentDtInit.Month < promotionsDates.CurrentDtEnd.Month;

            var month = monthOfDtInitIsLessOfDtEnd ? promotionsDates.CurrentDtInit.Month : promotionsDates.CurrentDtEnd.Month;

            var day = monthOfDtInitIsLessOfDtEnd ? DateTime.DaysInMonth(year, month) : promotionsDates.CurrentDtEnd.Day;

            var currentRevenue = await _orderDetailReadOnlyRepository.CalculateRevenueByMonth(new DateTime(year, month, 01), new DateTime(year, month, day));

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
            var lastPromotionRead = await _productPromotionReadDataHistoryRepositorie.GetLast();
            var productsPromotionsSummariesDates = await _productPromotionSummaryReadOnlyRepository.GetProductPromotionSummariesDates(cmd.PromotionCode);

            if (lastPromotionRead == null || !IsNewOrLastMonthOfCampaignValidator.Validate(productsPromotionsSummariesDates!))
                return;

            var year = productsPromotionsSummariesDates!.PreviousDtInit.Year;
            var month = productsPromotionsSummariesDates.PreviousDtInit.Month;

            var revenueByMonth = await _orderDetailReadOnlyRepository.CalculateRevenueByMonth(new DateTime(year, month, 01), new DateTime(year, month, DateTime.DaysInMonth(year, month)));

            cmd.SellersScore.ForEach(sellerScore =>
            {
                var revenueByMonthSeller = revenueByMonth?.FirstOrDefault(revenue => revenue.SellerId == sellerScore.SellerId);

                if (revenueByMonthSeller == null)
                    return;

                sellerScore.UpdateRevenueByMonth(revenueByMonthSeller!.Revenue, month);

                //if (revenueByMonthSeller!.Revenue >= sellerScore!.RevenueTarget)
                //    sellerScore.UpdateCouponsByRevenue();
            });
        }
    }
}