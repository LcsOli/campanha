using FluentValidation;
using Campaign.Shared.FluentValidator;
using Campaign.Pooling.DTO.Response.Order;

namespace Campaign.Pooling.Handlers.OrderDetail.GetOrdersDetail.Validator
{
    public class OrdersFindedValidator : FluentValidator<List<OrderDetailResponse>>
    {
        public OrdersFindedValidator()
        {
            RuleFor(o => o.Count)
                .GreaterThan(0)
                .WithMessage("Vendas não encontradas.");
        }
    }
}
