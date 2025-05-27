using Mail.Engine.Service.Function.Exceptions;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Mail.Engine.Service.Function.Middleware
{
    public class ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger) : IFunctionsWorkerMiddleware
    {
        private readonly ILogger<ErrorHandlerMiddleware> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);

                var problemDetails = new ProblemDetails
                {
                    Title = ErrorTitleProvider.GetErrorTitle(exception),
                    Detail = exception.Message,
                    Type = exception.GetType().Name,
                    Instance = context.GetHttpContext()!.Request.Path.ToString(),
                    Status = context.GetHttpContext()!.Response.StatusCode,
                    Extensions =
                {
                    ["traceID"] = Guid.NewGuid().ToString(), // Add the traceID here
                    ["raw"] = exception.ToString()
                }
                };

                // Write the problem details to the response
                await context.GetHttpContext()!.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
