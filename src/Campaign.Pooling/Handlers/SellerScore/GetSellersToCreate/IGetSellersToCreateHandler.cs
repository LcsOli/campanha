using Campaign.Pooling.Commands.Seller.Get;

namespace Campaign.Pooling.Handlers.Seller.GetSellersToCreate
{
    public interface IGetSellersToCreateHandler
    {
        Task<List<int>> Handle(GetSellerstoCreateCommand cmd);
    }
}
