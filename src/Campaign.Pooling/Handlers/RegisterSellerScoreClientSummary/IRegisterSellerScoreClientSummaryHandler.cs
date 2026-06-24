using Campaign.Processor.API.Commands.Summaries.Create;

namespace Campaign.Processor.API.Handlers.RegisterSellerScoreClientSummary
{
    public interface IRegisterSellerScoreClientSummaryHandler
    {
        Task Handle(ReactivatedsCommand cmd);
    }
}
