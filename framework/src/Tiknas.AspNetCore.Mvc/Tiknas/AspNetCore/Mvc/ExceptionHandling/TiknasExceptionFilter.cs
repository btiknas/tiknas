using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Tiknas.AspNetCore.ExceptionHandling;
using Tiknas.AspNetCore.Filters;
using Tiknas.Authorization;
using Tiknas.DependencyInjection;
using Tiknas.ExceptionHandling;
using Tiknas.Http;
using Tiknas.Json;

namespace Tiknas.AspNetCore.Mvc.ExceptionHandling;

public class TiknasExceptionFilter : IAsyncExceptionFilter, ITiknasFilter, ITransientDependency
{
    public virtual async Task OnExceptionAsync(ExceptionContext context)
    {
        if (!ShouldHandleException(context))
        {
            LogException(context, out _);
            return;
        }

        await HandleAndWrapException(context);
    }

    protected virtual bool ShouldHandleException(ExceptionContext context)
    {
        //TODO: Create DontWrap attribute to control wrapping..?

        if (context.ExceptionHandled)
        {
            return false;
        }

        if (context.ActionDescriptor.IsControllerAction() &&
            context.ActionDescriptor.HasObjectResult())
        {
            return true;
        }

        if (context.HttpContext.Request.CanAccept(MimeTypes.Application.Json))
        {
            return true;
        }

        if (context.HttpContext.Request.IsAjax())
        {
            return true;
        }

        return false;
    }

    protected virtual async Task HandleAndWrapException(ExceptionContext context)
    {
        //TODO: Trigger an TiknasExceptionHandled event or something like that.

        LogException(context, out var remoteServiceErrorInfo);

        await context.GetRequiredService<IExceptionNotifier>().NotifyAsync(new ExceptionNotificationContext(context.Exception));

        if (context.Exception is TiknasAuthorizationException)
        {
            await context.HttpContext.RequestServices.GetRequiredService<ITiknasAuthorizationExceptionHandler>()
                .HandleAsync(context.Exception.As<TiknasAuthorizationException>(), context.HttpContext);
        }
        else
        {
            if (!context.HttpContext.Response.HasStarted)
            {
                context.HttpContext.Response.Headers.Append(TiknasHttpConsts.TiknasErrorFormat, "true");
                context.HttpContext.Response.StatusCode = (int)context
                    .GetRequiredService<IHttpExceptionStatusCodeFinder>()
                    .GetStatusCode(context.HttpContext, context.Exception);
            }
            else
            {
                var logger = context.GetService<ILogger<TiknasExceptionFilter>>(NullLogger<TiknasExceptionFilter>.Instance)!;
                logger.LogWarning("HTTP response has already started, cannot set headers and status code!");
            }

            context.Result = new ObjectResult(new RemoteServiceErrorResponse(remoteServiceErrorInfo));
        }

        context.ExceptionHandled = true; //Handled!
    }

    protected virtual void LogException(ExceptionContext context, out RemoteServiceErrorInfo remoteServiceErrorInfo)
    {
        var exceptionHandlingOptions = context.GetRequiredService<IOptions<TiknasExceptionHandlingOptions>>().Value;
        var exceptionToErrorInfoConverter = context.GetRequiredService<IExceptionToErrorInfoConverter>();
        remoteServiceErrorInfo = exceptionToErrorInfoConverter.Convert(context.Exception, options =>
        {
            options.SendExceptionsDetailsToClients = exceptionHandlingOptions.SendExceptionsDetailsToClients;
            options.SendStackTraceToClients = exceptionHandlingOptions.SendStackTraceToClients;
            options.SendExceptionDataToClientTypes = exceptionHandlingOptions.SendExceptionDataToClientTypes;
        });

        var remoteServiceErrorInfoBuilder = new StringBuilder();
        remoteServiceErrorInfoBuilder.AppendLine($"---------- {nameof(RemoteServiceErrorInfo)} ----------");
        remoteServiceErrorInfoBuilder.AppendLine(context.GetRequiredService<IJsonSerializer>().Serialize(remoteServiceErrorInfo, indented: true));

        var logger = context.GetService<ILogger<TiknasExceptionFilter>>(NullLogger<TiknasExceptionFilter>.Instance)!;
        var logLevel = context.Exception.GetLogLevel();
        logger.LogWithLevel(logLevel, remoteServiceErrorInfoBuilder.ToString());
        logger.LogException(context.Exception, logLevel);
    }
}
