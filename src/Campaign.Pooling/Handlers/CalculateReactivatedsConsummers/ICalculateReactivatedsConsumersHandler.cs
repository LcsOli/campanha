using Campaign.Pooling.Commands.Consumers.Get;

namespace Campaign.Pooling.Handlers.CalculateReactivatedsConsummers
{
    public interface ICalculateReactivatedsConsumersHandler
    {
        Task Handle(CalculateReactivatedsConsumersCommand cmd);
    }
}
