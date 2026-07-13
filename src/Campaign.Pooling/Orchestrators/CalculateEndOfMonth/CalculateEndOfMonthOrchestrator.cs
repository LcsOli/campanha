using Campaign.Pooling.Repositories.Order.ReadOnly;
using Campaign.Processor.API.Handlers.Period.PeriodValidator;
using Campaign.Processor.API.Commands.Period.PeriodValidator.Validate;
using Campaign.Processor.API.Handlers.EndOfMonth.ScoreByOrderCanceled;
using Campaign.Processor.API.Handlers.EndOfMonth.ScoreByOrderItemRemoved;
using ScoreRemovedCommand = Campaign.Processor.API.Commands.EndOfMonth.ScoreByOrderRemoved.Create;
using ScoreCanceledCommand = Campaign.Processor.API.Commands.EndOfMonth.ScoreByOrderCanceled.Create;
using Campaign.Pooling.Services;

namespace Campaign.Processor.API.Orchestrators.CalculateEndOfMonth
{
    public class CalculateEndOfMonthOrchestrator : ICalculateEndOfMonthOrchestrator
    {
        public readonly IGetPeriodsHandler _periodValidatorHandler;
        public readonly IScoreByOrderRemovedHandler _scoreByOrderRemovedHandler;
        public readonly IScoreByOrderCanceledHandler _scoreByOrderCanceledHandler;

        public readonly IOrderDetailReadOnlyRepository _orderDetailReadOnlyRepository;
        public CalculateEndOfMonthOrchestrator(IGetPeriodsHandler periodValidatorHandler,
                                               IScoreByOrderRemovedHandler scoreByOrderRemovedHandler,
                                               IScoreByOrderCanceledHandler scoreByOrderCanceledHandler,
                                               IOrderDetailReadOnlyRepository orderDetailReadOnlyRepository)
        {
            _periodValidatorHandler = periodValidatorHandler;
            _scoreByOrderRemovedHandler = scoreByOrderRemovedHandler;
            _scoreByOrderCanceledHandler = scoreByOrderCanceledHandler;

            _orderDetailReadOnlyRepository = orderDetailReadOnlyRepository;
        }

        public async Task Execute(int promotionCode)
        {
            var periods = await _periodValidatorHandler.Handle(new GetByPromotionByYearCommand(promotionCode));
            
            var periodsService = new PeriodService(periods);


            //if (!periodsService.IsEndOfPeriod())
            //    return;

            //_scoreByOrderRemovedHandler.Handle(new ScoreRemovedCommand.CalculateCommand());

        }
    }
}
