using FluentValidation;
using Campaign.Shared.FluentValidator;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Handlers.Seller.GetSellers.Validator
{
    public class SallersWasFoundedValidator : FluentValidator<List<Entity.Seller>>
    {
        public SallersWasFoundedValidator()
        {
            RuleFor(s => s.Count())
                .GreaterThan(0)
                .WithMessage("Não foram encontrados vendedores para gerar score.");
        }
    }
}
