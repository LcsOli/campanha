namespace Campaign.Pooling.DTO.Response.Seller
{
    public record SellersConsumersResponse(int SellerId, 
                                           int ClientId, 
                                           DateTime RegisteredIn, 
                                           DateTime ReactivatedIn);
}
