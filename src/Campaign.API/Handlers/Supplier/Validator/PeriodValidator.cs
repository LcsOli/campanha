using FluentValidation;
using Campaign.Shared.FluentValidator;
using Campaign.Shared.DataBaseContext.Entities.Period;

namespace Campaign.API.Handlers.Supplier.Validator
{
    public class PeriodValidator : FluentValidator<Period>
    {
        public PeriodValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("Período não encontrado.");
        }
    }
}
