namespace Campaign.Pooling.Commands.Seller.Create
{
    public record SellersScoreToCreateCommand(string Name, int TeamId, int SellerId, string ManagerName);
}
