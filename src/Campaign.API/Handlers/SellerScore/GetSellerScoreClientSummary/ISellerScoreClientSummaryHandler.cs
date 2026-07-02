using Campaign.API.DTO.Page.Response;
using Campaign.API.Commands.SellerScore.Get;
using Campaign.API.DTO.SellerScoreProductSummary.Response;

namespace Campaign.API.Handlers.SellerScore.GetSellerScoreClientSummary
{
    public interface ISellerScoreClientSummaryHandler
    {
        Task<PageResponse<SellerScoreClientSummaryResponse.Customer, SellerScoreClientSummaryResponse.Resume>> Handle(GetSellerScoreClientSummaryCommand cmd);
    }
}
