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
                                      bool getPointsByTraining,
                                      DateTime? LastScoreByAccess,
                                      short QtyConsumersRegistereds,
                                      string RevenueTargetPercentage,
                                      short QtyConsumersReactivateds)
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Ranking { get; } = Ranking;
        public char GetPointsByTrainingDesc => getPointsByTraining ? 'S' : 'N';
    }
}
