using Campaign.Shared.Mappers;
using Campaign.API.DTO.SellerScore.Get;
using Campaign.API.Repositories.ProductPromotion.ReadOnly;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;
using Campaign.API.Handlers.SellerScore.GetSellersScores.Mapper;
using Campaign.API.Handlers.SellerScore.GetSellersScores.Validator;

namespace Campaign.API.Handlers.SellerScore.GetSellersScores
{
    public class GetSellersScoresHnadler : IGetSellersScoresHnadler
    {
        private readonly ISellerScoreReadOnlyRepository _sellerScoreReadOnlyRepository;
        public GetSellersScoresHnadler(ISellerScoreReadOnlyRepository sellerScoreReadOnlyRepository)
        {
            _sellerScoreReadOnlyRepository = sellerScoreReadOnlyRepository;
        }

        public async Task<List<SellerScoreResponse>> Handle()
        {
            var sellersScores = await _sellerScoreReadOnlyRepository.GetAll();

            new SellersScoresAreFindedValidator()
                .Validate(sellersScores);

            var mapper = new ToDTO();
            return mapper.Parse(new MapperParam<List<Entity.SellerScore>>(sellersScores));
        }
    }
}
