using Campaign.Pooling.Commands.Consumers.Get;

namespace Campaign.Pooling.Handlers.CalculateRegisteredsConsummers
{
    public interface ICalculateRegisteredsConsumersHandler
    {
        Task Handler(CalculateRegisteredsConsumersCommand cmd);
    }
}
