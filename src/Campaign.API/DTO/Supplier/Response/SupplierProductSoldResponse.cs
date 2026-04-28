namespace Campaign.API.DTO.Supplier.Response
{
    public record SupplierProductSoldResponse(string ProductName, int Quantity, decimal TotalValue);
}
