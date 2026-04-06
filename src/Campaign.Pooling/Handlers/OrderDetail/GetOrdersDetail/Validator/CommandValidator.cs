using FluentValidation;
using Campaign.Shared.FluentValidator;
using Campaign.Pooling.Commands.Orders.Get;

namespace Campaign.Pooling.Handlers.OrderDetail.GetOrdersDetail.Validator
{
    public class CommandValidator : FluentValidator<GetOrdersDetailCommand>
    {
        public CommandValidator()
        {
            RuleFor(o => o.promotionCode)
                .GreaterThan(0)
                .WithMessage("Código da promoção deve ser definida.");

            RuleFor(o => o.DtWeekToStartProcess)
                .GreaterThanOrEqualTo(default(DateTime))
                .WithMessage("Data inicial deve ser definida.");

            RuleFor(o => o.DtWeekToStopProcess)
                .GreaterThanOrEqualTo(default(DateTime))
                .WithMessage("Data final deve ser definida.");

            RuleFor(o => o.DtWeekToStartProcess)
                .LessThan(o => o.DtWeekToStopProcess)
                .WithMessage("Data inicial deve ser menor que a data final.");

            RuleFor(o => o.DtWeekToStopProcess)
                .GreaterThan(o => o.DtWeekToStartProcess)
                .WithMessage("Data final deve ser maior que a data final.");
        }
    }
}