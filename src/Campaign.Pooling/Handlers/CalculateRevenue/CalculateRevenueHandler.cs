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
            var currentVRevenue = await _orderDetailReadOnlyRepository.CalculateCurrentRevenue(cmd.PromotionCode);

            cmd.SellersScore.ForEach(sellerScore =>
            {
                var revenueSeller = currentVRevenue?.FirstOrDefault(revenue => revenue.SellerId == sellerScore.SellerId);
                sellerScore?.UpdateCurrentRevenue(revenueSeller!.Revenue);
            });


            var lastPromotionRead = await _productPromotionReadDataHistoryRepositorie.GetLast();

            if (lastPromotionRead != null)
            {
                var productsPromotionsSummariesDates = await _productPromotionSummaryReadOnlyRepository.GetOldAndNewProductPromotionsCodeDates(lastPromotionRead.PromotionCode,
                                                                                                                                               cmd.PromotionCode);

                var isDirerentMonths = productsPromotionsSummariesDates.OldProductPromotionInitDate.Month < productsPromotionsSummariesDates.NewProductPromotionInitDate.Month ||
                                       productsPromotionsSummariesDates.OldProductPromotionEndDate.Month < productsPromotionsSummariesDates.NewProductPromotionInitDate.Month;

                if (isDirerentMonths)
                {
                    var dateInit = new DateTime(productsPromotionsSummariesDates.OldProductPromotionInitDate.Year,
                                                productsPromotionsSummariesDates.OldProductPromotionInitDate.Month, 01);

                    var dateEnd = new DateTime(productsPromotionsSummariesDates.OldProductPromotionInitDate.Year,
                                               productsPromotionsSummariesDates.OldProductPromotionInitDate.Month,
                                               DateTime.DaysInMonth(dateInit.Year, dateInit.Month));

                    var revenueByMonth = await _orderDetailReadOnlyRepository.CalculateRevenueByMonth(dateInit, dateEnd);

                    cmd.SellersScore.ForEach(sellerScore =>
                    {
                        var revenueByMonthSeller = revenueByMonth?.FirstOrDefault(revenue => revenue.SellerId == sellerScore.SellerId);
                        sellerScore?.UpdateRevenueByMonth(revenueByMonthSeller!.Revenue, Math.Min(productsPromotionsSummariesDates.OldProductPromotionInitDate.Month, 
                                                                                                  productsPromotionsSummariesDates.NewProductPromotionEndDate.Month));
                    });
                }
            }
        }
    }
}
