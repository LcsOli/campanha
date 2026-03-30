using Campaign.Pooling.Repositories.SellerScore.ReadOnly;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;
using Campaign.Pooling.Handlers.SellerScore.GetSellersScore.Validator;

namespace Campaign.Pooling.Handlers.SellerScore.GetSellersScore
{
    public class GetSellerScoreHandler : IGetSellerScoreHandler
    {
        private ISellerScoreReadOnlyRepository _sellerScoreReadOnlyRepository;
        public GetSellerScoreHandler(ISellerScoreReadOnlyRepository sellerScoreReadOnlyRepository)
        {
            _sellerScoreReadOnlyRepository = sellerScoreReadOnlyRepository;
        }

        public async Task<List<Entity.SellerScore>> Handle()
        {
            var sellerScores = await _sellerScoreReadOnlyRepository.GetAll();

            new FindedSellerScoreValidator()
                .Validate(sellerScores);

            return sellerScores;
        }
    }
}
