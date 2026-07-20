using System.Net;
using Campaign.Shared.Exceptions;
using Campaign.Processor.API.Commands.AdjustScore;
using Campaign.Processor.API.Handlers.EndOfMonth.ScoreByOrderCanceled;
using Campaign.Processor.API.Handlers.EndOfMonth.ScoreByOrderItemRemoved;
using ScoreRemovedCommand = Campaign.Processor.API.Commands.EndOfMonth.ScoreByOrderRemoved.Create;
using ScoreCanceledCommand = Campaign.Processor.API.Commands.EndOfMonth.ScoreByOrderCanceled.Create;

namespace Campaign.Processor.API.Orchestrators.AdjustmentScore
{
    public class AdjustmentScoreOrchestrator : IAdjustmentScoreOrchestrator
    {
        public readonly IScoreByOrderRemovedHandler _scoreByOrderRemovedHandler;
        public readonly IScoreByOrderCanceledHandler _scoreByOrderCanceledHandler;

        

        public AdjustmentScoreOrchestrator(IScoreByOrderRemovedHandler scoreByOrderRemovedHandler, 
                                          IScoreByOrderCanceledHandler scoreByOrderCanceledHandler)
        {
            _scoreByOrderRemovedHandler = scoreByOrderRemovedHandler;
            _scoreByOrderCanceledHandler = scoreByOrderCanceledHandler;
        }

        public async Task Execute(AdjustScoreCommand cmd)
        {
            if (cmd.PromotionCode <= 0)
                throw new CompaignException(HttpStatusCode.BadRequest, "Defina o código da promoção.");

            await _scoreByOrderRemovedHandler.Handle(new ScoreRemovedCommand.CalculateCommand(cmd.PromotionCode));
            await _scoreByOrderCanceledHandler.Handle(new ScoreCanceledCommand.CalculateCommand(cmd.PromotionCode));
        }
    }
}
