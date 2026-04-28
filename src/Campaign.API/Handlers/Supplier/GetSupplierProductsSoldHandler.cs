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

            var supplierProducts = await _supplierProductSoldReadOnlyRepository.Get(cmd.SupplierId, cmd.InitIn, cmd.EndIn);

            new SupplierProductsFindedValidator().Validate(supplierProducts);

            return supplierProducts;
        }
    }
}
