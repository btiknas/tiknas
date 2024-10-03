using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Localization.Resources.TiknasUi;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Tiknas.AspNetCore.ExceptionHandling;
using Tiknas.AspNetCore.Mvc.UI.Theme.Shared.Views.Error;
using Tiknas.ExceptionHandling;

namespace Tiknas.AspNetCore.Mvc.UI.Theme.Shared.Controllers;

public class ErrorController : TiknasController
{
    protected readonly IExceptionToErrorInfoConverter ErrorInfoConverter;
    protected readonly IHttpExceptionStatusCodeFinder StatusCodeFinder;
    protected readonly IStringLocalizer<TiknasUiResource> Localizer;
    protected readonly TiknasErrorPageOptions TiknasErrorPageOptions;
    protected readonly IExceptionNotifier ExceptionNotifier;
    protected readonly TiknasExceptionHandlingOptions ExceptionHandlingOptions;

    public ErrorController(
        IExceptionToErrorInfoConverter exceptionToErrorInfoConverter,
        IHttpExceptionStatusCodeFinder httpExceptionStatusCodeFinder,
        IOptions<TiknasErrorPageOptions> tiknasErrorPageOptions,
        IStringLocalizer<TiknasUiResource> localizer,
        IExceptionNotifier exceptionNotifier,
        IOptions<TiknasExceptionHandlingOptions> exceptionHandlingOptions)
    {
        ErrorInfoConverter = exceptionToErrorInfoConverter;
        StatusCodeFinder = httpExceptionStatusCodeFinder;
        Localizer = localizer;
        ExceptionNotifier = exceptionNotifier;
        ExceptionHandlingOptions = exceptionHandlingOptions.Value;
        TiknasErrorPageOptions = tiknasErrorPageOptions.Value;
    }

    public virtual async Task<IActionResult> Index(int httpStatusCode)
    {
        var exHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

        var exception = exHandlerFeature != null
            ? exHandlerFeature.Error
            : new Exception(Localizer["UnhandledException"]);

        await ExceptionNotifier.NotifyAsync(new ExceptionNotificationContext(exception));

        var errorInfo = ErrorInfoConverter.Convert(exception, options =>
        {
            options.SendExceptionsDetailsToClients = ExceptionHandlingOptions.SendExceptionsDetailsToClients;
            options.SendStackTraceToClients = ExceptionHandlingOptions.SendStackTraceToClients;
            options.SendExceptionDataToClientTypes = ExceptionHandlingOptions.SendExceptionDataToClientTypes;
        });

        if (httpStatusCode == 0)
        {
            httpStatusCode = (int)StatusCodeFinder.GetStatusCode(HttpContext, exception);
        }

        HttpContext.Response.StatusCode = httpStatusCode;

        var page = GetErrorPageUrl(httpStatusCode);

        return View(page, new TiknasErrorViewModel
        {
            ErrorInfo = errorInfo,
            HttpStatusCode = httpStatusCode
        });
    }

    protected virtual string GetErrorPageUrl(int statusCode)
    {
        var page = TiknasErrorPageOptions.ErrorViewUrls.GetOrDefault(statusCode.ToString());

        if (string.IsNullOrWhiteSpace(page))
        {
            return "~/Views/Error/Default.cshtml";
        }

        return page;
    }
}
