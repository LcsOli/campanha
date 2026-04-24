using System.Net;
using Campaign.Shared.Exceptions;

namespace Campaign.Pooling.Services
{

    /*
        new PeriodRevenue(new(_year, 06, 01), new(_year, 06, 27)),
        new PeriodRevenue(new(_year, 06, 28), new(_year, 08, 01)),
        new PeriodRevenue(new(_year, 08, 02), new(_year, 08, 29)),
        new PeriodRevenue(new(_year, 08, 30), new(_year, 10, 03)),
        new PeriodRevenue(new(_year, 10, 04), new(_year, 10, 31))
     */

    public static class ProcessRevenueService
    {
        private static readonly short _year = 2025;
        private readonly static PeriodRevenue[] _periods = [

            new PeriodRevenue(new(_year, 06, 01), new(_year, 06, 28)),
            new PeriodRevenue(new(_year, 06, 29), new(_year, 08, 02)),
            new PeriodRevenue(new(_year, 08, 03), new(_year, 08, 30)),
            new PeriodRevenue(new(_year, 08, 31), new(_year, 09, 27)),
            new PeriodRevenue(new(_year, 09, 28), new(_year, 11, 01))

        ];

        public static bool IsEndOfPeriod(DateTime date)
        {
            return _periods.Select(p => p.end).Contains(date);
        }

        public static PeriodRevenue GetPeriod(DateTime date)
        {
            var period = _periods.FirstOrDefault(p => p.Init <= date && p.end >= date);

            if (period == null)
                throw new CompaignException(HttpStatusCode.InternalServerError, $"Data {date:dd/MM/yyyy} não representa um período.");

            return period;
        }
    }
}
