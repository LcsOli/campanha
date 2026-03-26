using FluentValidation;
using Campaign.Shared.FluentValidator;
using Campaign.Pooling.Commands.Seller.Get;

namespace Campaign.Pooling.Handlers.Seller.GetSellers.Validator
{
    public class ValidateListOfSellersIds : FluentValidator<GetSellersCommand>
    {
        public ValidateListOfSellersIds()
        {
            RuleFor(s => s.SellersIds.Count())
                .GreaterThan(0)
                .WithMessage("Identificadores de vendededores deve ser definido.");
        }
    }
}
