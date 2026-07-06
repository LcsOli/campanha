using Campaign.API.DTO.Page.Response;
using Campaign.API.Commands.SellerScore.Get;
using Campaign.API.DTO.SellerScore.Response;

namespace Campaign.API.Handlers.SellerScore.GetSellersScores
{
    public interface IGetSellersScoresHandler
    {
        Task<PageResponse<SellerScoreResponse>> Handle(GetSellersScoresByFiltersCommand cmd);
        Task<SellerScoreResponse> Handle(GetSellerScoreByIdCommand cmd);
    }
}
