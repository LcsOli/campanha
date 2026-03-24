using Microsoft.AspNetCore.Http;
using Campaign.API.Extensions.HttpCtx;
using Campaign.API.Configuration.Exceptions;

namespace Campaign.API.Configuration.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CompaignCollectionMessagesExceptions ex)
            {
                await context.ExceptionResponse(ex);
            }
            catch (CompaignException ex)
            {
                await context.ExceptionResponse(ex);
            }
            catch (Exception ex)
            {
                await context.ExceptionResponse();
            }
        }
    }
}
