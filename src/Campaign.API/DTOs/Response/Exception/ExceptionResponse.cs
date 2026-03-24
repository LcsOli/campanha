using System.Net;

namespace Campaign.API.DTOs.Response.Exception
{
    public record ExceptionResponse(HttpStatusCode Code, object Error);
}
