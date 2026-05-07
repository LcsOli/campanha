using Campaign.API.Commands.Supplier.Get;
using Campaign.API.DTO.Supplier.Response;
using Campaign.API.Handlers.Supplier.Validator;
using Campaign.API.Repositories.Period.ReadOnly;
using Campaign.API.Services.AuthenticatedUserCredencials;
using Campaign.API.Repositories.Supplier.SupplierProductSold.ReadOnly;

namespace Campaign.API.Handlers.Supplier
{
    public class GetSupplierProductsSoldHandler : IGetSupplierProductsSoldHandler
    {
        private readonly IPeriodReadOnlyRepository _periodReadOnlyRepository;
        private readonly IAuthenticatedUserCredencialsService _authenticatedUserCredencialsService;
        private readonly ISupplierProductSoldReadOnlyRepository _supplierProductSoldReadOnlyRepository;
        public GetSupplierProductsSoldHandler(IPeriodReadOnlyRepository periodReadOnlyRepository,
                                              IAuthenticatedUserCredencialsService authenticatedUserCredencialsService,
                                              ISupplierProductSoldReadOnlyRepository supplierProductSoldReadOnlyRepository)
        {
            _periodReadOnlyRepository = periodReadOnlyRepository;
            _authenticatedUserCredencialsService = authenticatedUserCredencialsService;
            _supplierProductSoldReadOnlyRepository = supplierProductSoldReadOnlyRepository;
        }

        public async Task<List<SupplierProductSoldResponse>> Handle(GetSupplierProductsSoldCommand cmd)
        {
            new DataValidator().Validate(cmd);

            var periodYear = await _periodReadOnlyRepository.GetFirst();

            new PeriodValidator().Validate(periodYear!);

            var initIn = new DateTime(periodYear!.Year, cmd.Month, 1);
            var endIn = initIn.AddMonths(1).AddSeconds(-1); 

            var supplierId = _authenticatedUserCredencialsService.SupplierId;
            var supplierProducts = await _supplierProductSoldReadOnlyRepository.Get(int.Parse(supplierId!), initIn, endIn);

            new SupplierProductsFindedValidator().Validate(supplierProducts);

            return supplierProducts;
        }
    }
}
