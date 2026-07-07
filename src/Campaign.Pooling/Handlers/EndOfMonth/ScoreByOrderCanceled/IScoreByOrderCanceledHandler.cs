using Campaign.Processor.API.Commands.EndOfMonth.ScoreByOrderCanceled.Create;

namespace Campaign.Processor.API.Handlers.EndOfMonth.ScoreByOrderCanceled
{
    public interface IScoreByOrderCanceledHandler
    {
        Task Handle(CalculateCommand cmd);
    }
}
