using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Noghte.BuildingBlock.ApiResponses;
using Noghte.BuildingBlock.Exceptions;
using System.Net;
using System.Text.Json;

namespace Noghte.BuildingBlock.Middlewares;

public static class CustomExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
    }
}

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _env;

    public CustomExceptionHandlerMiddleware(RequestDelegate next,
        IWebHostEnvironment env)
    {
        _next = next;
        _env = env;
    }

    public async Task Invoke(HttpContext context)
    {
        string message = null;
        HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            var innerException = exception.InnerException;

            if (innerException is not null && innerException is ValidationException)
            {
                httpStatusCode = HttpStatusCode.BadRequest;
                var validationException = (ValidationException)innerException;

                var result = new ConsumerValidationRejected
                {
                    Message = validationException.Errors,
                    StatusCode = ConsumerStatusCode.BadRequest,
                };

                message = JsonSerializer.Serialize(result);
            }
            else if (innerException is not null && innerException is AppException)
            {
                var appException = (AppException)innerException;

                var result = new ConsumerRejected
                {
                    Message = appException.AdditionalData.ToString(),
                    StatusCode = ConsumerStatusCode.ServerError
                };

                message = JsonSerializer.Serialize(result);
            }
            else
            {
                var result = new Dictionary<string, object>
                {
                    ["StatusCode"] = ConsumerStatusCode.ServerError,
                    ["Exception"] = exception.InnerException is not null ? exception.InnerException.Message : exception.Message,
                    ["StackTrace"] = exception.InnerException is not null ? exception.InnerException.StackTrace : exception.StackTrace,
                };

                message = JsonSerializer.Serialize(result);
            }

            await WriteToResponseAsync();
        }

        async Task WriteToResponseAsync()
        {
            if (context.Response.HasStarted)
                throw new InvalidOperationException(
                    "The response has already started, the http status code middleware will not be executed.");

            context.Response.StatusCode = (int)httpStatusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(message);
        }

        // catch (SecurityTokenExpiredException exception)
        // {
        //     _logger.LogError(exception, exception.Message);
        //     SetUnAuthorizeResponse(exception);
        //     await WriteToResponseAsync();
        // }
        // catch (UnauthorizedAccessException exception)
        // {
        //     _logger.LogError(exception, exception.Message);
        //     SetUnAuthorizeResponse(exception);
        //     await WriteToResponseAsync();
        // }

        // void SetUnAuthorizeResponse(Exception exception)
        // {
        //     httpStatusCode = HttpStatusCode.Unauthorized;
        //     apiStatusCode = ConsumerStatusCode.UnAuthorized;
        //
        //     if (_env.IsDevelopment())
        //     {
        //         var dic = new Dictionary<string, string>
        //         {
        //             ["Exception"] = exception.Message,
        //             ["StackTrace"] = exception.StackTrace
        //         };
        //         if (exception is SecurityTokenExpiredException tokenException)
        //             dic.Add("Expires", tokenException.Expires.ToString());
        //
        //         message = JsonConvert.SerializeObject(dic);
        //     }
        // }
    }
}