using Campaign.API.Configuration.Exceptions;
using Campaign.API.DTOs.Response. Exception;
using System.Net;

namespace Campaign.API.Extensions.HttpCtx
{
    public static class HttpContextExtension
    {
        public static async Task ExceptionResponse(this HttpContext context, CompaignCollectionMessagesExceptions ex)
        {
            context.Response.StatusCode = (short)ex.Code;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(new ExceptionResponse(ex.Code, ex.Messages));
        }

        public static async Task ExceptionResponse(this HttpContext context, CompaignException ex)
        {
            context.Response.StatusCode = (short)ex.Code;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(new ExceptionResponse(ex.Code, ex.Message));
        }

        public static async Task ExceptionResponse(this HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (short)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsJsonAsync(new ExceptionResponse(HttpStatusCode.InternalServerError, "Ocorreu um erro interno."));
        }
    }
}
