using Campaign.API.Commands.SellerScore.Create;
using Campaign.API.Repositories.ProductPromotion.WriteOnly;
using Campaign.API.Handlers.SellerScore.RegisterSellerScore.Mapper;
using Campaign.API.Handlers.SellerScore.RegisterSellerScore.Validator;

namespace Campaign.API.Handlers.SellerScore.RegisterSellerScore
{
    public class RegisterSellerScoreHandler : IRegisterSellerScoreHandler
    {
        private readonly ISellerScoreWriteOnlyRepository _sellerScoreWriteOnlyRepository;

        public RegisterSellerScoreHandler(ISellerScoreWriteOnlyRepository sellerScoreWriteOnlyRepository)
        {
            _sellerScoreWriteOnlyRepository = sellerScoreWriteOnlyRepository;
        }

        public async Task Handler(RegisterSellerScoreCommand cmd)
        {
            new RegisterSellerScoreValidater()
                .Validate(cmd);

            var entity = RegisterSellerScoreMapper.ToEntity(cmd);

            await _sellerScoreWriteOnlyRepository.Add(entity);
        }
    }
}
