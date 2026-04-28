using FluentValidation;
using Campaign.Shared.FluentValidator;
using Campaign.API.Commands.Supplier.Get;

namespace Campaign.API.Handlers.Supplier.Validator
{
    public class DataValidator : FluentValidator<GetSupplierProductsSoldCommand>
    {
        public DataValidator()
        {
            RuleFor(s => s.SupplierId)
                .GreaterThan(0)
                .WithMessage("O Id do fornecedor deve ser maior que zero.");

            RuleFor(s => s.Month)
                .LessThanOrEqualTo(12)
                .WithMessage("O mês deve estar entre 1 e 12.");
        }
    }
}
