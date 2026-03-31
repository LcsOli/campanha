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

            RuleFor(o => o.initIn)
                .GreaterThanOrEqualTo(default(DateTime))
                .WithMessage("Data inicial deve ser definida.");

            RuleFor(o => o.endIn)
                .GreaterThanOrEqualTo(default(DateTime))
                .WithMessage("Data final deve ser definida.");

            RuleFor(o => o.initIn)
                .LessThan(o => o.endIn)
                .WithMessage("Data inicial deve ser menor que a data final.");

            RuleFor(o => o.endIn)
                .GreaterThan(o => o.initIn)
                .WithMessage("Data final deve ser maior que a data final.");
        }
    }
}