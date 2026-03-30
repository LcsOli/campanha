using FluentValidation;
using Campaign.Shared.FluentValidator;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Handlers.SellerScore.GetSellersScore.Validator
{
    public class FindedSellerScoreValidator : FluentValidator<List<Entity.SellerScore>>
    {
        public FindedSellerScoreValidator()
        {
            RuleFor(sellersScore => sellersScore.Count())
                .GreaterThan(0)
                .WithMessage("Nenhum score de vendedor encontrado.");
        }
    }
}
