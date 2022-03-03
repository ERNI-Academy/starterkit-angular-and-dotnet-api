using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using App.API.Helpers;
using App.API.Services.CustomExceptions;

using Microsoft.AspNetCore.Http;

namespace App.API.Middleware;

/// <summary>
/// The error handler middleware.
/// </summary>
public class ErrorHandlerMiddleware
{
    /// <summary>
    /// The next.
    /// </summary>
    private readonly RequestDelegate next;

    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorHandlerMiddleware"/> class.
    /// </summary>
    /// <param name="next">
    /// The next.
    /// </param>
    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    /// <summary>
    /// The invoke.
    /// </summary>
    /// <param name="context">The http context.</param>
    /// <returns>The result.</returns>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            response.StatusCode = error switch
            {
                AuthenticationException _ or LogEventException _ or UserException _ or ApiException _ => (int)HttpStatusCode.BadRequest,// custom application error
                UnauthorizedException _ => (int)HttpStatusCode.Unauthorized,
                KeyNotFoundException _ => (int)HttpStatusCode.NotFound,// not found error
                ForbiddenException _ => (int)HttpStatusCode.Forbidden,
                _ => (int)HttpStatusCode.InternalServerError,// unhandled error
            };
            var result = JsonSerializer.Serialize(new ErrorResponse { Message = error?.Message });
            await response.WriteAsync(result);
        }
    }
}
