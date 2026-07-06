using static Campaign.API.DTO.SellerManager.Response.SellerManagerRevenueInfosResponse;

namespace Campaign.API.DTO.SellerManager.Response
{
    public record SellerManagerRevenueInfosResponse(List<RevenueInfo> RevenueInfos)
    {
        public record RevenueInfo(string SellerName, int SellerId, decimal RevenueTarget, decimal CurrentRevenue)
        {
            public string RevenueTargetPercentage => RevenueTarget > 0 ? $"{CurrentRevenue * 100 / RevenueTarget:F2}%" : "0%";
        }
    }
}
