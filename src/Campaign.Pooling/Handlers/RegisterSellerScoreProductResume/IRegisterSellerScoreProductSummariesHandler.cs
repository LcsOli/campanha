using Campaign.Processor.API.Commands.Summaries.Create;

namespace Campaign.Processor.API.Handlers.RegisterSellerScoreProductResume
{
    public interface IRegisterSellerScoreProductSummariesHandler
    {
        Task Handle(RegisterSellerScoreProductSummariesCommand cmd);
    }
}
