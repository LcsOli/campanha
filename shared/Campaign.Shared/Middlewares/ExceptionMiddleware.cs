using Microsoft.AspNetCore.Http;
using Campaign.Shared.Exceptions;
using Campaign.Shared.Extensions.HttpCtx;

namespace Campaign.Shared.Middlewares
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
