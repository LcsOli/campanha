using Campaign.Shared.Mappers;
using Campaign.Shared.Extensions.Enums;
using Campaign.Shared.Enums.SellerScoreConsumerType;
using Campaign.Shared.DataBaseContext.Entities.Customer;
using Campaign.API.DTO.SellerScoreProductSummary.Response;
using Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries;

namespace Campaign.API.Handlers.SellerScore.GetSellerScoreClientSummary.Mapper
{
    public class ToDTO : Mapper<SellerScoreClientSummaryResponse, List<SellerScoreClientsSummary>, List<Customer>>
    {
        public override SellerScoreClientSummaryResponse Parse(MapperParam<List<SellerScoreClientsSummary>> param1, MapperParam<List<Customer>> param2)
        {
            var summary = param1.Model;
            var customer = param2.Model;

            var customers = summary.Select(x =>
            {
                var customerName = customer.SingleOrDefault(c => c.Id == x.CustomerId)?.Name;

                return new SellerScoreClientSummaryResponse.Customer(x.CustomerId, customerName!, x.CustomerSalesEventType.GetTranslatedDescription(), x.RegisteredIn, x.ReactivatedIn);
            });

            var registereds = summary.Where(x => x.CustomerSalesEventType == CustomerSalesEventType.Registered);
            var registeredsResume = new SellerScoreClientSummaryResponse.Registered(registereds.Sum(x => x.Score), registereds.Count());

            var reactivateds = summary.Where(x => x.CustomerSalesEventType == CustomerSalesEventType.Reactivated);
            var reactivatedsResume = new SellerScoreClientSummaryResponse.Reactivated(reactivateds.Sum(x => x.Score), reactivateds.Count());

            var resumes = new SellerScoreClientSummaryResponse.Resume(reactivatedsResume, registeredsResume);

            return new SellerScoreClientSummaryResponse([.. customers], resumes);
        }
    }
}
