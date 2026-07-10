using Campaign.Processor.API.Commands.EndOfMonth.ScoreByOrderRemoved.Create;

namespace Campaign.Processor.API.Handlers.EndOfMonth.ScoreByOrderItemRemoved
{
    public interface IScoreByOrderRemovedHandler
    {
        Task Handle(CalculateCommand cmd);
    }
}
