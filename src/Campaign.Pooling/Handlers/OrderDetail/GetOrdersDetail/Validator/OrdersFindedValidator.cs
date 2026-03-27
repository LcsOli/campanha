using FluentValidation;
using Campaign.Shared.FluentValidator;
using Entity = Campaign.Shared.DataBaseContext.Entities.Order;

namespace Campaign.Pooling.Handlers.OrderDetail.GetOrdersDetail.Validator
{
    public class OrdersFindedValidator : FluentValidator<List<Entity.OrderDetail>>
    {
        public OrdersFindedValidator()
        {
            RuleFor(o => o.Count)
                .GreaterThan(0)
                .WithMessage("Vendas não encontradas.");
        }
    }
}
