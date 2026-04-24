namespace Campaign.API.DTO.SellerScore.Response
{
    public record SellerScoreResponse(int SellerId,
                                      decimal Score,
                                      string Ranking,
                                      string TeamName,
                                      string SellerName,
                                      decimal RevenueTarget,
                                      decimal CurrentRevenue,
                                      string SellerManagerName,
                                      string RevenueTargetPercentage);
}
