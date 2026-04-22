using Campaign.API.DTO.SellerScore.Get;

namespace Campaign.API.Handlers.SellerScore.GetSellersScores
{
    public interface IGetSellersScoresHnadler
    {
        Task<List<SellerScoreResponse>> Handle();
    }
}
