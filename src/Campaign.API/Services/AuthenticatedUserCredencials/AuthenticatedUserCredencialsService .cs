using System.Net;
using System.Security.Claims;
using Campaign.Shared.Exceptions;

namespace Campaign.API.Services.AuthenticatedUserCredencials
{
    public class AuthenticatedUserCredencialsService : IAuthenticatedUserCredencialsService
    {
        private readonly IHttpContextAccessor _http;
        public AuthenticatedUserCredencialsService(IHttpContextAccessor http) => _http = http;

        private ClaimsPrincipal? User => _http.HttpContext?.User
            ?? throw new CompaignException(HttpStatusCode.BadRequest, "Role não foi encontrada no jwt token.");

        public string? SupplierId => User?.FindFirstValue("supplierId")
           ?? throw new CompaignException(HttpStatusCode.BadRequest, "ID do fornecedor não foi encontrado no jwt token.");

        public string? SellerId => User?.FindFirstValue("sellerId")
           ?? throw new CompaignException(HttpStatusCode.BadRequest, "ID do RCA não foi encontrado no jwt token.");
    }
}
