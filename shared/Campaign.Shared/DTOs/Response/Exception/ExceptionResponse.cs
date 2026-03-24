using System.Net;

namespace Campaign.Shared.DTOs.Response.Exception
{
    public record ExceptionResponse(HttpStatusCode Code, object Error);
}
