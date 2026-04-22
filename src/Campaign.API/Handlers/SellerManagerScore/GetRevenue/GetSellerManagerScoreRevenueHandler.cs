using Campaign.Shared.Mappers;
using Campaign.API.DTO.SellerManagerScore.Response;
using Campaign.API.Repositories.SellerManager.ReadOnly;
using Campaign.API.Handlers.SellerManager.GetRevenue.Validator;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;
using Campaign.API.Handlers.SellerManagerScore.GetRevenue.Mapper;

namespace Campaign.API.Handlers.SellerManager.GetRevenue
{
    public class GetSellerManagerScoreRevenueHandler : IGetSellerManagerScoreRevenueHandler
    {
        private readonly ISellerManagerReadOnlyRepository _sellerManagerReadOnlyRepository;
        public GetSellerManagerScoreRevenueHandler(ISellerManagerReadOnlyRepository sellerManagerReadOnlyRepository)
        {
            _sellerManagerReadOnlyRepository = sellerManagerReadOnlyRepository;
        }

        public async Task<List<SellerManagerScoreResponse>> Handle()
        {
            var sellersManagers = await _sellerManagerReadOnlyRepository.GetAll();

            new SellersManagersScoreAreFindedValidator()
                .Validate(sellersManagers);

            var mapper = new ToDTO();
            return mapper.Parse(new MapperParam<List<Entity.SellerManagerScore>>(sellersManagers));
        }
    }
}
