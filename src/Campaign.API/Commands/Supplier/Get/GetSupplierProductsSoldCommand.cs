namespace Campaign.API.Commands.Supplier.Get
{
    public record GetSupplierProductsSoldCommand(int SupplierId, DateTime InitIn, DateTime EndIn);
}
