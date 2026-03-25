using Campaign.Pooling.Commands.Seller.Create;
using Campaign.Pooling.Repositories.SellerScore.WriteOnly;
using Campaign.Pooling.Handlers.Seller.InsertSeller.Mapper;
using Campaign.Pooling.Handlers.Seller.InsertSeller.Validator;

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

            var validator = new ValidateSallersScoreToCreate();
            cmd.SellersToCreate.ForEach(sellerScore => validator.Validate(sellerScore));

            var entities = InsertSellerScoreMapper.ToEntities(cmd.SellersToCreate);

            await _sellerScoreWriteOnlyRepository.AddAsync(entities);
        }
    }
}
