using System.Net;
using Campaign.Shared.Exceptions;
using Campaign.Shared.DataBaseContext.Entities.Period;

namespace Campaign.Pooling.Services
{
    public class PeriodService
    {
        private readonly List<Period> _periods;
        public PeriodService(List<Period> periods)
        {
            _periods = periods;
        }

        public bool IsEndOfPeriod(DateTime date)
        {
            return _periods.Select(p => p.End).Contains(date);
        }

        public Period GetPeriod(DateTime date)
        {
            var period = _periods.FirstOrDefault(p => p.Init <= date && p.End >= date);

            if (period == null)
                throw new CompaignException(HttpStatusCode.InternalServerError, $"Data {date:dd/MM/yyyy} não representa um período.");

            return period;
        }
    }
}
