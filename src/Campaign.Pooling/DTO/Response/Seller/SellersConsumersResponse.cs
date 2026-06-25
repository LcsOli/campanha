namespace Campaign.Pooling.DTO.Response.Seller
{
    public record SellersConsumersResponse(int SellerId, 
                                           int CustomerId, 
                                           DateTime RegisteredIn);
}
