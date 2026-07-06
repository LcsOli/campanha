namespace Campaign.API.Services.Token
{
    public interface IJwtToken
    {
        string Generate(string userId, string name, int sellerId, string role);
        string Generate(string userId, string name, string supplierId, string role);
        string Generate(string userId, string name, string teamId, string sellerId, string role);
    }
}
