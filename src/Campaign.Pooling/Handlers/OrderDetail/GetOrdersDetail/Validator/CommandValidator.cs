using FluentValidation;
using Campaign.Shared.FluentValidator;
using Campaign.Pooling.Commands.Orders.Get;

namespace Campaign.Pooling.Handlers.OrderDetail.GetOrdersDetail.Validator
{
    public class CommandValidator : FluentValidator<GetOrdersDetailCommand>
    {
        public CommandValidator()
        {
            RuleFor(o => o.productsIds.Length)
                .GreaterThan(0)
                .WithMessage("Identificadores dos produtos devem ser informados.");

            RuleFor(o => o.initIn)
                .GreaterThanOrEqualTo(default(DateTime))
                .WithMessage("Data inicial deve ser definida.");

            RuleFor(o => o.endIn)
                .GreaterThanOrEqualTo(default(DateTime))
                .WithMessage("Data final deve ser definida.");

            RuleFor(o => o.initIn)
                .GreaterThan(o => o.endIn)
                .WithMessage("Data inicial deve ser maior que a data final.");
        }
    }
}