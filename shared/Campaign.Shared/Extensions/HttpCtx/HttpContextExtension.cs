using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Campaign.Shared.Exceptions;
using Campaign.Shared.DTOs.Response.Exception;

namespace Campaign.Shared.Extensions.HttpCtx
{
    public static class HttpContextExtension
    {
        public static async Task ExceptionResponse(this HttpContext context, CompaignCollectionMessagesExceptions ex)
        {
            context.Response.StatusCode = (short)ex.Code;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonSerializer.Serialize(new ExceptionResponse(ex.Code, ex.Messages)));
        }

        public static async Task ExceptionResponse(this HttpContext context, CompaignException ex)
        {
            context.Response.StatusCode = (short)ex.Code;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonSerializer.Serialize(new ExceptionResponse(ex.Code, ex.Message)));
        }

        public static async Task ExceptionResponse(this HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (short)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(JsonSerializer.Serialize(new ExceptionResponse(HttpStatusCode.InternalServerError, "Ocorreu um erro interno.")));
        }
    }
}