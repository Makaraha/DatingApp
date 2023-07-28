using System.Net;
using System.Text.Json;
using Azure;
using Common.Exceptions.ServerExceptions;

namespace DatingApp.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch(BaseServerException e)
            {
                await ExecuteExceptionAsync(context, e);
            }

            catch (Exception e)
            {
                await ExecuteExceptionAsync(context,new UnhandledException(e.Message, e));
            }
        }

        private async Task ExecuteExceptionAsync(HttpContext context, BaseServerException exception)
        {
            var result = Results.Problem(
                title: exception.StatusCode.ToString(),
                detail: exception.Message,
                statusCode: (int)exception.StatusCode);

            await result.ExecuteAsync(context);
        }
    }
}
