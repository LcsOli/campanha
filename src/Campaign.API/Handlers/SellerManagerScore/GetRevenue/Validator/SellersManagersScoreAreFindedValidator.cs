using FluentValidation;
using Campaign.Shared.FluentValidator;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.API.Handlers.SellerManager.GetRevenue.Validator
{
    public class SellersManagersScoreAreFindedValidator : FluentValidator<List<Entity.SellerManagerScore>>
    {
        //TODO - Padronizar validações utilizando FluentValidator
        public SellersManagersScoreAreFindedValidator()
        {
            RuleFor(s => s.Count)
                .GreaterThan(0)
                .WithMessage("Não foram encontrados score de supervisores.");
        }
    }
}
