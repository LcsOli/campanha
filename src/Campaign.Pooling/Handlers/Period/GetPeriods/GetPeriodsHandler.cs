using System.Net;
using Campaign.Shared.Exceptions;
using Campaign.Pooling.Repositories.Period.ReadOnly;
using Entity = Campaign.Shared.DataBaseContext.Entities.Period;
using Campaign.Pooling.Repositories.ProductPromotionSummary.ReadOnly;
using Campaign.Processor.API.Commands.Period.PeriodValidator.Validate;

namespace Campaign.Processor.API.Handlers.Period.PeriodValidator
{
    public class GetPeriodsHandler : IGetPeriodsHandler
    {
        private readonly IPeriodReadOnlyRepository _periodReadOnlyRepository;
        //private readonly IProductPromotionSummaryReadOnlyRepository _productPromotionSummaryReadOnlyRepository;

        public GetPeriodsHandler(IPeriodReadOnlyRepository periodReadOnlyRepository
                                 /*IProductPromotionSummaryReadOnlyRepository productPromotionSummaryReadOnlyRepository*/)
        {
            _periodReadOnlyRepository = periodReadOnlyRepository;
            //_productPromotionSummaryReadOnlyRepository = productPromotionSummaryReadOnlyRepository;
        }

        public async Task<List<Entity.Period>> Handle(GetByPromotionByYearCommand cmd)
        {
            //var promotionsDates = await _productPromotionSummaryReadOnlyRepository.GetProductPromotionSummariesDates(cmd.PromotionCode);
            var periods = await _periodReadOnlyRepository.GetByYear(cmd.Year);

            if (!periods.Any())
                throw new CompaignException(HttpStatusCode.NotFound, "Períodos não encontrados.");

            return periods;
        }
    }
}
