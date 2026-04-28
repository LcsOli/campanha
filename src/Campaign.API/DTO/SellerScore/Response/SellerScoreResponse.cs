using System.Text.Json.Serialization;

namespace Campaign.API.DTO.SellerScore.Response
{
    public record SellerScoreResponse(int SellerId,
                                      short Coupons,
                                      decimal Score,
                                      string Ranking,
                                      string TeamName,
                                      string SellerName,
                                      decimal RevenueTarget,
                                      decimal CurrentRevenue,
                                      string SellerManagerName,
                                      string RevenueTargetPercentage)
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Ranking { get; } = Ranking;
    }
}
