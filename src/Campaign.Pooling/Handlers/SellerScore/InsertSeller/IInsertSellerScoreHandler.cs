using Campaign.Pooling.Commands.Seller.Create;

namespace Campaign.Pooling.Handlers.Seller.InsertSeller
{
    public interface IInsertSellerScoreHandler
    {
        Task Handle(CreateSellerScoreCommand cmd);
    }
}
