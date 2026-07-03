using Campaign.Processor.API.Commands.ScoreRemoved.Create;
using Campaign.Processor.API.Handlers.ScoreCanceled.Validator;

namespace Campaign.Processor.API.Handlers.ScoreRemoved
{
    public class ScoreRemovedHandler : IScoreRemovedHandler
    {
        public ScoreRemovedHandler()
        {
            
        }

        public async Task Handle(RegisterScoreRemovedCommand cmd)
        {
            new RegisterScoreRemovedDataValidator()
                .Validate(cmd);

        }
    }
}
