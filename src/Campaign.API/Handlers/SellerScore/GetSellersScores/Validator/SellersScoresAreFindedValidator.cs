using FluentValidation;
using Campaign.Shared.FluentValidator;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.API.Handlers.SellerScore.GetSellersScores.Validator
{
    public class SellersScoresAreFindedValidator : FluentValidator<List<Entity.SellerScore>>
    {
        public SellersScoresAreFindedValidator()
        {
            RuleFor(s => s.Count)
                .GreaterThan(0)
                .WithMessage("Não foram encontrados Scores de vendedores.");
        }
    }
}
