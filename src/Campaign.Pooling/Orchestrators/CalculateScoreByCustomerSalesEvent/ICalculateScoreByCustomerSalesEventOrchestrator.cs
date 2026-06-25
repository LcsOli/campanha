using Campaign.Processor.API.Commands.Consumers.Get;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Processor.API.Orchestrators.CalculateScoreByCustomerSalesEvent
{
    public interface ICalculateScoreByCustomerSalesEventOrchestrator
    {
        Task Execute(CalculateScoreCustomerReactivatedsSalesEventCommand cmd);
        Task Execute(CalculateScoreCustomerRegisteredsSalesEventCommand cmd);
    }
}
