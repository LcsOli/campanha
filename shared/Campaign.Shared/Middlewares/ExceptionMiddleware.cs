using Campaign.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using StackTraceInternalLibrary.Service;
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

        public async Task InvokeAsync(HttpContext context, IRegisterTraceService stackTraceService)
        {
            try
            {
                await _next(context);
            }
            catch (CompaignCollectionMessagesExceptions ex)
            {
                stackTraceService.RegisterTrace(ex);
                await context.ExceptionResponse(ex);
            }
            catch (CompaignException ex)
            {
                stackTraceService.RegisterTrace(ex);
                await context.ExceptionResponse(ex);
            }
            catch (Exception ex)
            {
                stackTraceService.RegisterTrace(ex);
                await context.ExceptionResponse();
            }
        }
    }
}
