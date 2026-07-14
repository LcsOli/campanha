using Campaign.Processor.API.Commands.AdjustScore;
using Campaign.Pooling.Repositories.Period.ReadOnly;
using Campaign.Processor.API.Orchestrators.AdjustmentScore;

namespace Campaign.Processor.API.Orchestrators.CalculateEndOfMonth
{
    public class CalculateEndOfMonthOrchestrator : ICalculateEndOfMonthOrchestrator
    {
        private readonly IPeriodReadOnlyRepository _periodReadOnlyRepository;
        public readonly IAdjustmentScoreOrchestrator _adjustmentScoreOrchestrator;
        public CalculateEndOfMonthOrchestrator(IPeriodReadOnlyRepository periodReadOnlyRepository,
                                               IAdjustmentScoreOrchestrator adjustmentScoreOrchestrator)
        {
            _periodReadOnlyRepository = periodReadOnlyRepository;
            _adjustmentScoreOrchestrator = adjustmentScoreOrchestrator;
        }

        public async Task Execute(int promotionCode)
        {
            var promotionsCodes = await _periodReadOnlyRepository.GetPromotionsCodesByPeriod(promotionCode);
            var isEndOfPeriod = promotionsCodes.Length > 0;

            if (!isEndOfPeriod)
                return;

            foreach (var code in promotionsCodes)
                await _adjustmentScoreOrchestrator.Execute(new AdjustScoreCommand(code));
        }
    }
}
