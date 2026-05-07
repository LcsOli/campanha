namespace Campaign.API.Services.AuthenticatedUserCredencials
{
    public interface IAuthenticatedUserCredencialsService
    {
        string? SupplierId { get; }
        string? SellerId { get; }
    }
}
