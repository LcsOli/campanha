using Campaign.Pooling.Commands.Consumers.Get;

namespace Campaign.Pooling.Handlers.CalculateReactivatedsConsummers
{
    public interface ICalculateReactivatedsConsumersHandler
    {
        void Handle(CalculateReactivatedsConsumersCommand cmd);
    }
}
