using System.Net;

namespace Campaign.API.DTOs.Exception
{
    public record ExceptionResponse(HttpStatusCode Code, object Error);
}
