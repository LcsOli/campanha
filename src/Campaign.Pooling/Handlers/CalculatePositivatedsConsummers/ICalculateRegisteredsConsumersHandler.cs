using Campaign.Pooling.Commands.Consumers.Get;

namespace Campaign.Pooling.Handlers.CalculatePositivatedsConsummers
{
    public interface ICalculateRegisteredsConsumersHandler
    {
        Task Handler(CalculateRegisteredsConsumersCommand cmd);
    }
}
