using Campaign.Processor.API.Commands.AdjustScore;

namespace Campaign.Processor.API.Orchestrators.AdjustmentScore
{
    public interface IAdjustmentScoreOrchestrator
    {
        Task Execute(AdjustScoreCommand cmd);
    }
}
