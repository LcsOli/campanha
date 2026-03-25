using FluentValidation;
using Campaign.Shared.FluentValidator;
using Campaign.Pooling.Commands.Seller.Get;

namespace Campaign.Pooling.Handlers.Seller.GetSellersToCreate.Validator
{
    public class ValidateListOfSellersIds : FluentValidator<GetSellerstoCreateCommand>
    {
        public ValidateListOfSellersIds()
        {
            RuleFor(s => s.SellersIds.Count())
                .GreaterThan(0)
                .WithMessage("Identificadores de vendededores deve ser definido.");
        }
    }
}
