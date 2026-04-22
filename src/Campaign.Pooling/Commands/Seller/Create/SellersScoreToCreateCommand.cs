namespace Campaign.Pooling.Commands.Seller.Create
{
    public record SellersScoreToCreateCommand(int TeamId, 
                                              string Name, 
                                              int SellerId, 
                                              string ManagerName,
                                              int SellerManagerId);
}
