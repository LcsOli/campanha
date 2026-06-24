using Campaign.Processor.API.Commands.Summaries.Create;

namespace Campaign.Processor.API.Handlers.RegisterSellerScoreProductSummary
{
    public interface IRegisterSellerScoreProductSummaryHandler
    {
        Task Handle(RegisterSellerScoreProductSummaryCommand cmd);
    }
}
