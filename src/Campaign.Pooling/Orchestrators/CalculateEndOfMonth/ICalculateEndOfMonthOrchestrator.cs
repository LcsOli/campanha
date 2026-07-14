namespace Campaign.Processor.API.Orchestrators.CalculateEndOfMonth
{
    public interface ICalculateEndOfMonthOrchestrator
    {
        Task Execute(int promotionCode);
    }
}
