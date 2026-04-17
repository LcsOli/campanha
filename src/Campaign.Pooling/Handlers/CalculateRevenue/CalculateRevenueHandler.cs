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

        /*
-----------------------------------------------------------------
            
            -ScoreSupervisor (id, faturamentoTotal)
            -ScoreSupervisorRefSupervisores (idScoreSupervisor, idSupervisor)
            -Supervisor (id, CodSupervisor, Nome)

            ScoreSupervisor 1 <-> N SupervisorAuxiliar 
            ScoreSupervisorRefSupervisores N <-> N Supervisor

            ScoreSupervisor
                -Id: 1
                -FaturamentoTotal: 100_000
                -ScoreSupervisorRefSupervisoresId: 1
                

                -Id: 2
                -FaturamentoTotal: 250_000
                -ScoreSupervisorRefSupervisoresId: 2


                -Id: 3
                -FaturamentoTotal: 450_000
                -ScoreSupervisorRefSupervisoresId: 3


                -Id: 4
                -FaturamentoTotal: 150_000
                -ScoreSupervisorRefSupervisoresId: 4

-----------------------------------------------------------------

            ScoreSupervisorRefSupervisores
                -IdScoreSupervisor: 1
                -IdSupervisor: 1
                
                -IdScoreSupervisor: 2
                -IdSupervisor: 3

                -IdScoreSupervisor: 3
                -IdSupervisor: 1

                -IdScoreSupervisor: 4
                -ScoreSupervisorRefSupervisoresId: 5

-----------------------------------------------------------------

            Supervisores
                -Id: 1
                -CodSupervisor: 25
                -Nome: José
         
                -Id: 2
                -CodSupervisor: 45
                -Nome: José

                -Id: 3
                -CodSupervisor: 34
                -Nome: Rodolfo

                -Id: 4
                -CodSupervisor: 36
                -Nome: Rodolfo

                -Id: 5
                -CodSupervisor: 55
                -Nome: Paula

-----------------------------------------------------------------
         */

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
                sellerScore?.UpdateCurrentRevenue(revenueSeller!.Revenue);
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
                sellerScore?.UpdateRevenueByMonth(revenueByMonthSeller!.Revenue, month);

                if (revenueByMonthSeller!.Revenue >= sellerScore!.RevenueTarget)
                    sellerScore.UpdateCouponsByRevenue();
            });
        }
    }
}