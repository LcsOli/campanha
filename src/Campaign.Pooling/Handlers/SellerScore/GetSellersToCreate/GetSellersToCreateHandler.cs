using Campaign.Pooling.Commands.Seller.Get;
using Campaign.Pooling.Repositories.SellerScore.ReadOnly;
using Campaign.Pooling.Handlers.Seller.GetSellersToCreate.Validator;

namespace Campaign.Pooling.Handlers.Seller.GetSellersToCreate
{
    public class GetSellerstoCreateHandler : IGetSellersToCreateHandler
    {
        private readonly ISellerScoreReadOnlyRepository _sellerScoreReadOnlyRepository;
        public GetSellerstoCreateHandler(ISellerScoreReadOnlyRepository sellerScoreReadOnlyRepository)
        {
            _sellerScoreReadOnlyRepository = sellerScoreReadOnlyRepository;
        }

        public async Task<List<int>> Handle(GetSellerstoCreateCommand cmd)
        {
            new CommandValidator().Validate(cmd);
            var sellersIds =  await _sellerScoreReadOnlyRepository.GetRegisteredsById(cmd.SellersIds);

            return [.. cmd.SellersIds.Except(sellersIds)];
        }
    }
}
