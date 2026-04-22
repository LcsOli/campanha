using System.Net;
using Campaign.Shared.Exceptions;
using Campaign.API.Commands.User.Update;
using Campaign.API.Repositories.User.ReadOnly;
using Campaign.API.Repositories.ProductPromotion.ReadOnly;
using Campaign.API.Repositories.ProductPromotion.WriteOnly;
using Campaign.Pooling.Repositories.ProductPromotionSummary.ReadOnly;

namespace Campaign.API.Handlers.User.InsertScoreByAccess
{
    public class InsertScoreByAccessHandler : IInsertScoreByAccessHandler
    {
        private readonly int _scoreToAdd = 10_000;

        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly ISellerScoreReadOnlyRepository _sellerScoreReadOnlyRepository;
        private readonly ISellerScoreWriteOnlyRepository _sellerScoreWriteOnlyRepository;
        private readonly IProductPromotionSummaryReadOnlyRepository _productPromotionSummaryReadOnlyRepository;
        public InsertScoreByAccessHandler(IUserReadOnlyRepository userReadOnlyRepository,
                                          ISellerScoreReadOnlyRepository sellerScoreReadOnlyRepository,
                                          ISellerScoreWriteOnlyRepository sellerScoreWriteOnlyRepository,
                                          IProductPromotionSummaryReadOnlyRepository productPromotionSummaryReadOnlyRepository)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _sellerScoreReadOnlyRepository = sellerScoreReadOnlyRepository;
            _sellerScoreWriteOnlyRepository = sellerScoreWriteOnlyRepository;
            _productPromotionSummaryReadOnlyRepository = productPromotionSummaryReadOnlyRepository;
        }

        public async Task Handle(InsertScoreByAccessCommand cmd)
        {
            var user = await _userReadOnlyRepository.GetByDocument(cmd.Document) ??
                throw new CompaignException(HttpStatusCode.NotFound, "Usuário não encontrado.");

            if (user.Roles != Shared.Enums.Role.Roles.User)
                return;

            var sellerScore = await _sellerScoreReadOnlyRepository.GetBySellerId(user.SellerId!.Value) ??
                throw new CompaignException(HttpStatusCode.NotFound, "Score não encontrado para este vendedor.");

            var dateToFindProductPromotio = sellerScore.LastScoreByAccess ?? DateTime.Now;

            var productPromotion = await _productPromotionSummaryReadOnlyRepository.GetByPeriod(dateToFindProductPromotio);

            if (productPromotion == null)
                return;

            var hasRegisteredScore = sellerScore.LastScoreByAccess.HasValue &&
                                     (
                                       productPromotion.InitIn <= sellerScore.LastScoreByAccess &&
                                       productPromotion.EndIn >= sellerScore.LastScoreByAccess
                                     );

            if (hasRegisteredScore)
                return;

            sellerScore.UpdateScore(_scoreToAdd);
            sellerScore.UpdateLastScoreByAccess();

            _sellerScoreWriteOnlyRepository.Update(sellerScore);
        }
    }
}