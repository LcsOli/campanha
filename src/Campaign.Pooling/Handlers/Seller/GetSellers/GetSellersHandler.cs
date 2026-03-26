using Campaign.Pooling.Commands.Seller.Get;
using Campaign.Pooling.Handlers.Seller.GetSellers.Validator;
using Campaign.Pooling.Repositories.Sellers.Seller.ReadOnly;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Handlers.Seller.GetSellers
{
    public class GetSellersHandler : IGetSellersHandler
    {
        private readonly ISellerReadOnlyRepository _sellerReadOnlyRepository;
        public GetSellersHandler(ISellerReadOnlyRepository sellerReadOnlyRepository)
        {
            _sellerReadOnlyRepository = sellerReadOnlyRepository;
        }

        public async Task<List<Entity.Seller>> Handle(GetSellersCommand cmd)
        {
            new ValidateListOfSellersIds().Validate(cmd);

            var sallers = await _sellerReadOnlyRepository.GetSellersByIds(cmd.SellersIds);

            new ValidateWhetherSallersWasFounded().Validate(sallers);

            return sallers;
        }
    }
}
