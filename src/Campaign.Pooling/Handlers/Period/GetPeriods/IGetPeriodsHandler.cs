using Entity = Campaign.Shared.DataBaseContext.Entities.Period;
using Campaign.Processor.API.Commands.Period.PeriodValidator.Validate;

namespace Campaign.Processor.API.Handlers.Period.PeriodValidator
{
    public interface IGetPeriodsHandler
    {
        Task<List<Entity.Period>> Handle(GetByPromotionByYearCommand cmd);
    }
}
