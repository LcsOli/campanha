
using Campaign.API.DTO.SellerManager.Response;

namespace Campaign.API.Handlers.SellerManager.GetRevenue
{
    public interface ISellerManagerScoreRevenueHandler
    {
        Task<List<SellerManagerScoreResponse>> Handle();
    }
}
