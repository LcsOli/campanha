using Campaign.API.DTO.Page.Response;
using Campaign.API.Commands.SellerScore.Get;
using Campaign.API.DTO.SellerScoreProductSummary.Response;

namespace Campaign.API.Handlers.SellerScore.GetSellerScoreProductSummary
{
    public interface ISellerScoreProductSummaryHandler
    {
        Task<PageResponse<SellerScoreProductSummaryResponse.Product, SellerScoreProductSummaryResponse.Resume>> Handle(GetSellerScoreProductSummaryCommand cmd);
    }
}
