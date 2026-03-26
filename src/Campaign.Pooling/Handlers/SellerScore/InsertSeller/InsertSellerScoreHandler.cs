using Campaign.Shared.Mappers;
using Campaign.Pooling.Commands.Seller.Create;
using Campaign.Pooling.Repositories.SellerScore.WriteOnly;
using Campaign.Pooling.Handlers.Seller.InsertSeller.Validator;
using Campaign.Pooling.Handlers.SellerScore.InsertSeller.Mapper;

namespace Campaign.Pooling.Handlers.Seller.InsertSeller
{
    public class InsertSellerScoreHandler : IInsertSellerScoreHandler
    {
        private readonly ISellerScoreWriteOnlyRepository _sellerScoreWriteOnlyRepository;
        public InsertSellerScoreHandler(ISellerScoreWriteOnlyRepository sellerScoreWriteOnlyRepository)
        {
            _sellerScoreWriteOnlyRepository = sellerScoreWriteOnlyRepository;
        }

        public async Task Handle(CreateSellerScoreCommand cmd)
        {
            if (cmd.SellersToCreate.Count <= 0)
                return;

            cmd.SellersToCreate.ForEach(sellerScore => new SallersScoreValidator().Validate(sellerScore));

            var mapperParam = new MapperParamImplement<List<SellersScoreToCreateCommand>>(cmd.SellersToCreate);
            var entities = new ToEntities().Parse(mapperParam);

            await _sellerScoreWriteOnlyRepository.AddAsync(entities);
        }
    }
}
