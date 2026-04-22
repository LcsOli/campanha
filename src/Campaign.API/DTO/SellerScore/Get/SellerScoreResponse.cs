namespace Campaign.API.DTO.SellerScore.Get
{
    public record SellerScoreResponse(int SellerId,
                                      decimal Score,
                                      string Ranking,
                                      string TeamName,
                                      string SellerName,
                                      decimal CurrentRevenue,
                                      string SellerManagerName,
                                      string RevenueTargetPercentage);
}
