using FluentValidation;
using Campaign.Shared.FluentValidator;
using Campaign.Pooling.Commands.Orders.Get;

namespace Campaign.Pooling.Handlers.OrderDetail.GetOrdersDetail.Validator
{
    public class CommandValidator : FluentValidator<GetOrdersDetailCommand>
    {
        public CommandValidator()
        {
            RuleFor(o => o.PromotionCode)
                .GreaterThan(0)
                .WithMessage("Código da promoção deve ser definida.");
        }
    }
}