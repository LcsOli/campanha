using Campaign.Processor.API.DTO.Response.Seller;
using Campaign.Shared.Enums.SellerScoreConsumerType;
using Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Processor.API.Commands.Summaries.Create
{
    public record RegisterRegisteredsCommand : RegisterCommand
    {
        public List<RegisteredsConsumerResponse> RegisteredsConsumers { get; }
        public RegisterRegisteredsCommand(int PromotionCode,
                                          List<SellerScore> SellersScores,
                                          List<RegisteredsConsumerResponse> RegisteredsConsumers) : base(PromotionCode, SellersScores)
        {
            this.RegisteredsConsumers = RegisteredsConsumers;
        }
    }
}
