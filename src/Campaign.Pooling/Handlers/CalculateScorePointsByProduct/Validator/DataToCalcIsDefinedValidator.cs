using FluentValidation;
using Campaign.Shared.FluentValidator;
using Campaign.Pooling.Commands.CalculateScoreByProduct;

namespace Campaign.Pooling.Handlers.CalculateScorePoints.Validator
{
    public class DataToCalcIsDefinedValidator : FluentValidator<CalculateScoreByProductCommand>
    {
        public DataToCalcIsDefinedValidator()
        {

            RuleFor(c => c.SellersScores.Count)
                .GreaterThan(0)
                .WithMessage("Lista de vendedores devem ser definidos.");

            RuleFor(c => c.PromotionCode)
                .GreaterThan(0)
                .WithMessage("Detalhes da venda devem ser definidos.");
        }
    }
}
