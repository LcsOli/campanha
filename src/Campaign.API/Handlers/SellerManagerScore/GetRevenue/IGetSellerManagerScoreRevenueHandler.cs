using Campaign.API.DTO.SellerManagerScore.Response;

namespace Campaign.API.Handlers.SellerManager.GetRevenue
{
    public interface IGetSellerManagerScoreRevenueHandler
    {
        Task<List<SellerManagerScoreResponse>> Handle();
    }
}
