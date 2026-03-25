namespace Campaign.Pooling.Commands.Seller.Create
{
    public record SellersScoreToCreateCommand(string Name,
                                              int SellerId,
                                              string ManagerName);
}
