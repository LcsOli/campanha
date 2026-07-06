namespace Campaign.API.DTO.SellerManager.Response
{
    public record SellerManagerScoreResponse(string Ranking, string Name, decimal TargetRevenue, decimal CurrentRevenue);
}
