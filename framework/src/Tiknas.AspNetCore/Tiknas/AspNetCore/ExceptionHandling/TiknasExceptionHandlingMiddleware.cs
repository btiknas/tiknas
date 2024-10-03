using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Tiknas.AspNetCore.Middleware;
using Tiknas.AspNetCore.Mvc;
using Tiknas.Authorization;
using Tiknas.DependencyInjection;
using Tiknas.ExceptionHandling;
using Tiknas.Http;
using Tiknas.Json;

namespace Tiknas.AspNetCore.ExceptionHandling;

public class TiknasExceptionHandlingMiddleware : TiknasMiddlewareBase, ITransientDependency
{
    private readonly ILogger<TiknasExceptionHandlingMiddleware> _logger;

    private readonly Func<object, Task> _clearCacheHeadersDelegate;

    public TiknasExceptionHandlingMiddleware(ILogger<TiknasExceptionHandlingMiddleware> logger)
    {
        _logger = logger;

        _clearCacheHeadersDelegate = ClearCacheHeaders;
    }

    public async override Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            // We can't do anything if the response has already started, just abort.
            if (context.Response.HasStarted)
            {
                _logger.LogWarning("An exception occurred, but response has already started!");
                throw;
            }

            if (context.Items["_TiknasActionInfo"] is TiknasActionInfoInHttpContext actionInfo)
            {
                if (actionInfo.IsObjectResult) //TODO: Align with TiknasExceptionFilter.ShouldHandleException!
                {
                    await HandleAndWrapException(context, ex);
                    return;
                }
            }

            throw;
        }
    }

    private async Task HandleAndWrapException(HttpContext httpContext, Exception exception)
    {
        _logger.LogException(exception);

        await httpContext
            .RequestServices
            .GetRequiredService<IExceptionNotifier>()
            .NotifyAsync(
                new ExceptionNotificationContext(exception)
            );

        if (exception is TiknasAuthorizationException)
        {
            await httpContext.RequestServices.GetRequiredService<ITiknasAuthorizationExceptionHandler>()
                .HandleAsync(exception.As<TiknasAuthorizationException>(), httpContext);
        }
        else
        {
            var errorInfoConverter = httpContext.RequestServices.GetRequiredService<IExceptionToErrorInfoConverter>();
            var statusCodeFinder = httpContext.RequestServices.GetRequiredService<IHttpExceptionStatusCodeFinder>();
            var jsonSerializer = httpContext.RequestServices.GetRequiredService<IJsonSerializer>();
            var exceptionHandlingOptions = httpContext.RequestServices.GetRequiredService<IOptions<TiknasExceptionHandlingOptions>>().Value;

            httpContext.Response.Clear();
            httpContext.Response.StatusCode = (int)statusCodeFinder.GetStatusCode(httpContext, exception);
            httpContext.Response.OnStarting(_clearCacheHeadersDelegate, httpContext.Response);
            httpContext.Response.Headers.Append(TiknasHttpConsts.TiknasErrorFormat, "true");
            httpContext.Response.Headers.Append("Content-Type", "application/json");

            await httpContext.Response.WriteAsync(
                jsonSerializer.Serialize(
                    new RemoteServiceErrorResponse(
                        errorInfoConverter.Convert(exception, options =>
                        {
                            options.SendExceptionsDetailsToClients = exceptionHandlingOptions.SendExceptionsDetailsToClients;
                            options.SendStackTraceToClients = exceptionHandlingOptions.SendStackTraceToClients;
                            options.SendExceptionDataToClientTypes = exceptionHandlingOptions.SendExceptionDataToClientTypes;
                        })
                    )
                )
            );
        }
    }

    private Task ClearCacheHeaders(object state)
    {
        var response = (HttpResponse)state;

        response.Headers[HeaderNames.CacheControl] = "no-cache";
        response.Headers[HeaderNames.Pragma] = "no-cache";
        response.Headers[HeaderNames.Expires] = "-1";
        response.Headers.Remove(HeaderNames.ETag);

        return Task.CompletedTask;
    }
}
