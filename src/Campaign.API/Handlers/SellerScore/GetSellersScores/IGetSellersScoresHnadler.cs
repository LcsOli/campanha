using Campaign.API.Commands.SellerScore.Get;
using Campaign.API.DTO.Page.Response;
using Campaign.API.DTO.SellerScore.Response;

namespace Campaign.API.Handlers.SellerScore.GetSellersScores
{
    public interface IGetSellersScoresHnadler
    {
        Task<List<SellerScoreResponse>> Handle();
        Task<PageResponse<SellerScoreResponse>> Handle(GetSellersScoresByTeamCommand cmd);
    }
}
