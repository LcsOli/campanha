using FluentValidation;
using Campaign.Shared.FluentValidator;
using Campaign.Pooling.Commands.CalculateScore;

namespace Campaign.Pooling.Handlers.CalculateScore.Validator
{
    public class PromotionCodeValidator : FluentValidator<CalculateScoreCommand>
    {
        public PromotionCodeValidator()
        {
            RuleFor(c => c.PromotionCode)
                .GreaterThan(0)
                .WithMessage("Código da promoção deve ser definido.");
        }
    }
}
