using FluentValidation;
using Campaign.Shared.FluentValidator;
using Campaign.Processor.API.Commands.EndOfMonth.ScoreByOrderRemoved.Create;

namespace Campaign.Processor.API.Handlers.EndOfMonth.ScoreByOrderItemRemoved.Validator
{
    public class RegisterScoreRemovedDataValidator : FluentValidator<CalculateCommand>
    {
        public RegisterScoreRemovedDataValidator()
        {
            //RuleFor(x => x.Orders.Count)
            //    .GreaterThan(0)
            //    .WithMessage("Lista de vendedores devem ser definidos.");
        }
    }
}
