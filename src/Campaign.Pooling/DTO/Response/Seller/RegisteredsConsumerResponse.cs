using Campaign.Pooling.DTO.Response.Seller;
using Campaign.Shared.Enums.SellerScoreConsumerType;

namespace Campaign.Processor.API.DTO.Response.Seller
{
    public record RegisteredsConsumerResponse : SellersConsumersResponse
    {
        public CustomerSalesEventType CustomerSalesEvent = CustomerSalesEventType.Registered;

        public RegisteredsConsumerResponse(int SellerId,
                                           int ClientId,
                                           DateTime RegisteredIn,
                                           DateTime ReactivatedIn) : base(SellerId, ClientId, RegisteredIn, ReactivatedIn)
        { }
    }
}
