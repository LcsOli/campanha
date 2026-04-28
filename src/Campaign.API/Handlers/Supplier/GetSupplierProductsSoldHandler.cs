using Campaign.API.Commands.Supplier.Get;
using Campaign.API.DTO.Supplier.Response;
using Campaign.API.Handlers.Supplier.Validator;
using Campaign.API.Repositories.Supplier.SupplierProductSold.ReadOnly;

namespace Campaign.API.Handlers.Supplier
{
    public class GetSupplierProductsSoldHandler : IGetSupplierProductsSoldHandler
    {
        private readonly ISupplierProductSoldReadOnlyRepository _supplierProductSoldReadOnlyRepository;
        public GetSupplierProductsSoldHandler(ISupplierProductSoldReadOnlyRepository supplierProductSoldReadOnlyRepository)
        {
            _supplierProductSoldReadOnlyRepository = supplierProductSoldReadOnlyRepository;
        }

        public async Task<List<SupplierProductSoldResponse>> Handle(GetSupplierProductsSoldCommand cmd)
        {
            new DataValidator().Validate(cmd);

            var initIn = new DateTime(2025, cmd.Month, 1);
            var endIn = initIn.AddMonths(1).AddSeconds(-1); 

            var supplierProducts = await _supplierProductSoldReadOnlyRepository.Get(cmd.SupplierId, initIn, endIn);

            new SupplierProductsFindedValidator().Validate(supplierProducts);

            return supplierProducts;
        }
    }
}
