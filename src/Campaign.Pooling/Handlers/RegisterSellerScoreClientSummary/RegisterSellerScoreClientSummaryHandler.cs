using Campaign.Processor.API.Commands.Summaries.Create;
using Campaign.Processor.API.Repositories.SellerScoreClientSummary.WriteOnly;

namespace Campaign.Processor.API.Handlers.RegisterSellerScoreClientSummary
{
    public class RegisterSellerScoreClientSummaryHandler : IRegisterSellerScoreClientSummaryHandler
    {
        private readonly ISellerScoreClientSummaryRepository _sellerScoreClientSummaryRepository;
        public RegisterSellerScoreClientSummaryHandler(ISellerScoreClientSummaryRepository sellerScoreClientSummaryRepository)
        {
            _sellerScoreClientSummaryRepository = sellerScoreClientSummaryRepository;
        }

        public async Task Handle(ReactivatedsCommand cmd)
        {

        }
    }
}
