using Campaign.API.Commands.SellerManager.Get;
using Campaign.API.DTO.SellerManager.Response;

namespace Campaign.API.Handlers.SellerManagerScore.TargetManager
{
    public interface ISellerManagerRevenueTargetHandler
    {
        Task<SellerManagerRevenueInfosResponse> Handle(GetTargetRevenueCommand cmd);
    }
}
