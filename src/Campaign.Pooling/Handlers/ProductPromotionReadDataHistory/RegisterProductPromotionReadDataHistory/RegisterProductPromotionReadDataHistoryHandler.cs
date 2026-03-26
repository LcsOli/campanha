using Campaign.Pooling.Commands.ProductPromotionReadHistory.Create;
using Campaign.Pooling.Repositories.ProductPromotionReadDataHistory.WriteOnly;
using Campaign.Pooling.Handlers.ProductPromotionReadDataHistory.RegisterNewHistory.Mapper;
using Campaign.Pooling.Handlers.ProductPromotionReadDataHistory.RegisterNewHistory.Validator;

namespace Campaign.Pooling.Handlers.ProductPromotionReadDataHistory.RegisterNewHistory
{
    public class RegisterProductPromotionReadDataHistoryHandler : IRegisterProductPromotionReadDataHistoryHandler
    {
        private readonly IProductPromotionReadDataHistoryWriteOnlyRepository _productPromotionReadDataHistoryWriteOnlyRepository;
        public RegisterProductPromotionReadDataHistoryHandler(IProductPromotionReadDataHistoryWriteOnlyRepository productPromotionReadDataHistoryWriteOnlyRepository)
        {
            _productPromotionReadDataHistoryWriteOnlyRepository = productPromotionReadDataHistoryWriteOnlyRepository;
        }

        public async Task Handle(RegisterProductPromotionReadDataHistoryCommand cmd)
        {
            new CommandValidator().Validate(cmd);
            await _productPromotionReadDataHistoryWriteOnlyRepository.AddAsync(ToEntity.Parse(cmd));
        }
    }
}
