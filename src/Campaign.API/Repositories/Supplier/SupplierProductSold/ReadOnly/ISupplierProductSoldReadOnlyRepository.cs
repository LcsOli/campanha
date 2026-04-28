using Campaign.API.DTO.Supplier.Response;

namespace Campaign.API.Repositories.Supplier.SupplierProductSold.ReadOnly
{
    public interface ISupplierProductSoldReadOnlyRepository
    {
        Task<List<SupplierProductSoldResponse>> Get(int supplierId, DateTime initIn, DateTime endIn);
    }
}
