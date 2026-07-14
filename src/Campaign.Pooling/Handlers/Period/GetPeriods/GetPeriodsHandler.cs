using System.Net;
using Campaign.Shared.Exceptions;
using Campaign.Pooling.Repositories.Period.ReadOnly;
using Entity = Campaign.Shared.DataBaseContext.Entities.Period;
using Campaign.Processor.API.Commands.Period.PeriodValidator.Validate;

namespace Campaign.Processor.API.Handlers.Period.PeriodValidator
{
    public class GetPeriodsHandler : IGetPeriodsHandler
    {
        private readonly IPeriodReadOnlyRepository _periodReadOnlyRepository;

        public GetPeriodsHandler(IPeriodReadOnlyRepository periodReadOnlyRepository)
        {
            _periodReadOnlyRepository = periodReadOnlyRepository;
        }

        public async Task<List<Entity.Period>> Handle(GetByYearCommand cmd)
        {
            var periods = await _periodReadOnlyRepository.GetByYear(cmd.Year);

            if (!periods.Any())
                throw new CompaignException(HttpStatusCode.NotFound, "Períodos não encontrados.");

            return periods;
        }
    }
}
