using FluentValidation;
using Campaign.Shared.FluentValidator;
using Campaign.API.DTO.Supplier.Response;

namespace Campaign.API.Handlers.Supplier.Validator
{
    public class SupplierProductsFindedValidator : FluentValidator<List<SupplierProductSoldResponse>>
    {
        public SupplierProductsFindedValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("Não foram encontrados produtos vendidos para o fornecedor.");
        }
    }
}
