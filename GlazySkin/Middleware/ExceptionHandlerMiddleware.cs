using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Servicies.Exceptions;

namespace GlazySkin.Middleware
{
    public class ExceptionHandlerMiddleware(RequestDelegate _next)
    {
        public async Task InvokeAsync(HttpContext httpContext, ILogger<ExceptionHandlerMiddleware> logger, ProblemDetailsFactory problemDetailsFactory)
        {
            try
            {
                logger.LogInformation("Request has happened with {RequestPath}", httpContext.Request.Path.Value);
                await _next.Invoke(httpContext);

            }
            catch (Exception exception)
            {
                logger.LogError(exception,
                    "Error has happened with {RequestPath}, " +
                    "the message is {ErrorMessage}", httpContext.Request.Path.Value, exception.Message);
                var httpStatusCode = exception switch
                {
                    NotFoundException notFoundException => notFoundException.ErrorCode switch
                    {
                        ErrorCode.Gone => StatusCodes.Status410Gone,
                        _ => StatusCodes.Status500InternalServerError
                    },
                    _ => StatusCodes.Status500InternalServerError
                };
                ProblemDetails problemDetails;
                switch (exception)
                {
                    case NotFoundException notFoundException:
                        problemDetails = problemDetailsFactory.CreateProblemDetails(httpContext, httpStatusCode, notFoundException.Message);
                        logger.LogError(notFoundException, "Service Exception occured");
                        break;
                    default:
                        problemDetails = problemDetailsFactory.CreateProblemDetails(httpContext, httpStatusCode,
                            "Unhendled error! Please contcat us.", detail: exception.Message);
                        logger.LogError(exception, "Unhandled Exception occured");
                        break;
                }
                httpContext.Response.StatusCode = httpStatusCode;
                await httpContext.Response.WriteAsJsonAsync(problemDetails);

            }
        }
    }
}
