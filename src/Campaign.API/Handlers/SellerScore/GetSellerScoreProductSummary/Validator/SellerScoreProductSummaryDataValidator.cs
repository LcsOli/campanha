using FluentValidation;
using Campaign.Shared.FluentValidator;
using Campaign.API.Commands.SellerScore.Get;

namespace Campaign.API.Handlers.SellerScore.GetSellerScoreProductSummary.Validator
{
    public class SellerScoreProductSummaryDataValidator : FluentValidator<GetSellerScoreProductSummaryCommand>
    {
        public SellerScoreProductSummaryDataValidator()
        {
            RuleFor(x => x.SellerId)
                .GreaterThan(0)
                .WithMessage("Identificador do RCA é obrigatório.");

            RuleFor(x => x.PromotionCode)
                .GreaterThan(0)
                .WithMessage("Código da promoção é obrigatório.");
        }
    }
}
