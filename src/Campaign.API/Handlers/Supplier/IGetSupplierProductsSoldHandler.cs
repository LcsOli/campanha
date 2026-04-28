using Campaign.API.Commands.Supplier.Get;
using Campaign.API.DTO.Supplier.Response;

namespace Campaign.API.Handlers.Supplier
{
    public interface IGetSupplierProductsSoldHandler
    {
        Task<List<SupplierProductSoldResponse>> Handle(GetSupplierProductsSoldCommand cmd);
    }
}
