namespace Campaign.Program.Models
{
    public record SellersScoreToRegisterModel(int SellerId, 
                                              string SellerName, 
                                              int SellerManagerId, 
                                              string SellerManagerName);
}
