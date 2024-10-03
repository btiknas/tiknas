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

public class TiknasExceptionPageFilter : IAsyncPageFilter, ITiknasFilter, ITransientDependency
{
    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        return Task.CompletedTask;
    }

    public virtual async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        if (context.HandlerMethod == null || !ShouldHandleException(context))
        {
            await next();
            return;
        }

        var pageHandlerExecutedContext = await next();
        if (pageHandlerExecutedContext.Exception == null)
        {
            return;
        }

        await HandleAndWrapException(pageHandlerExecutedContext);
    }

    protected virtual bool ShouldHandleException(PageHandlerExecutingContext context)
    {
        //TODO: Create DontWrap attribute to control wrapping..?

        if (context.ActionDescriptor.IsPageAction() &&
            ActionResultHelper.IsObjectResult(context.HandlerMethod!.MethodInfo.ReturnType, typeof(void)))
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

    protected virtual async Task HandleAndWrapException(PageHandlerExecutedContext context)
    {
        //TODO: Trigger an TiknasExceptionHandled event or something like that.

        if (context.ExceptionHandled)
        {
            return;
        }

        var exceptionHandlingOptions = context.GetRequiredService<IOptions<TiknasExceptionHandlingOptions>>().Value;
        var exceptionToErrorInfoConverter = context.GetRequiredService<IExceptionToErrorInfoConverter>();
        var remoteServiceErrorInfo = exceptionToErrorInfoConverter.Convert(context.Exception!, options =>
       {
           options.SendExceptionsDetailsToClients = exceptionHandlingOptions.SendExceptionsDetailsToClients;
           options.SendStackTraceToClients = exceptionHandlingOptions.SendStackTraceToClients;
           options.SendExceptionDataToClientTypes = exceptionHandlingOptions.SendExceptionDataToClientTypes;
       });

        var logLevel = context.Exception!.GetLogLevel();

        var remoteServiceErrorInfoBuilder = new StringBuilder();
        remoteServiceErrorInfoBuilder.AppendLine($"---------- {nameof(RemoteServiceErrorInfo)} ----------");
        remoteServiceErrorInfoBuilder.AppendLine(context.GetRequiredService<IJsonSerializer>().Serialize(remoteServiceErrorInfo, indented: true));

        var logger = context.GetService<ILogger<TiknasExceptionPageFilter>>(NullLogger<TiknasExceptionPageFilter>.Instance)!;
        logger.LogWithLevel(logLevel, remoteServiceErrorInfoBuilder.ToString());

        logger.LogException(context.Exception!, logLevel);

        await context.GetRequiredService<IExceptionNotifier>().NotifyAsync(new ExceptionNotificationContext(context.Exception!));

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
                    .GetStatusCode(context.HttpContext, context.Exception!);
            }
            else
            {
                logger.LogWarning("HTTP response has already started, cannot set headers and status code!");
            }

            context.Result = new ObjectResult(new RemoteServiceErrorResponse(remoteServiceErrorInfo));
        }

        context.ExceptionHandled = true; //Handled!
    }
}
