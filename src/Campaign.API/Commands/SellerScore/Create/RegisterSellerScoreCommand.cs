namespace Campaign.API.Commands.SellerScore.Create
{
    public record RegisterSellerScoreCommand(int SellerId, string Name, int TeamId, int SellerManagerId, string SellerManagerName);
}
