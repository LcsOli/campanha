namespace Campaign.API.DTO.SellerManagerScore.Response
{
    public record SellerManagerScoreResponse(string Ranking, string Name, decimal TargetRevenue, decimal CurrentRevenue);
}
