using FluentValidation;
using Campaign.Shared.FluentValidator;
using Campaign.API.DTO.SellerManagerScore.Response;
using Campaign.API.DTO.SellerScore.Response;

namespace Campaign.API.Handlers.SellerScore.GetSellersScores.Validator
{
    public class SellersScoresAreFindedValidator : FluentValidator<List<SellerScoreResponse>>
    {
        public SellersScoresAreFindedValidator()
        {
            RuleFor(s => s.Count)
                .GreaterThan(0)
                .WithMessage("Não foram encontrados Scores de vendedores.");
        }
    }
}
