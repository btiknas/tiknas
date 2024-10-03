using System;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Mvc.AntiForgery;

public class TiknasAutoValidateAntiforgeryTokenAuthorizationFilter : TiknasValidateAntiforgeryTokenAuthorizationFilter, ITransientDependency
{
    private readonly TiknasAntiForgeryOptions _options;

    public TiknasAutoValidateAntiforgeryTokenAuthorizationFilter(
        IAntiforgery antiforgery,
        TiknasAntiForgeryCookieNameProvider antiForgeryCookieNameProvider,
        IOptions<TiknasAntiForgeryOptions> options,
        ILogger<TiknasValidateAntiforgeryTokenAuthorizationFilter> logger)
        : base(
            antiforgery,
            antiForgeryCookieNameProvider,
            logger)
    {
        _options = options.Value;
    }

    protected override bool ShouldValidate(AuthorizationFilterContext context)
    {
        if (!_options.AutoValidate)
        {
            return false;
        }

        if (context.ActionDescriptor.IsControllerAction())
        {
            var controllerType = context.ActionDescriptor
                .AsControllerActionDescriptor()
                .ControllerTypeInfo
                .AsType();

            if (!_options.AutoValidateFilter(controllerType))
            {
                return false;
            }
        }

        if (IsIgnoredHttpMethod(context))
        {
            return false;
        }

        return base.ShouldValidate(context);
    }

    protected virtual bool IsIgnoredHttpMethod(AuthorizationFilterContext context)
    {
        return context.HttpContext
            .Request
            .Method
            .ToUpperInvariant()
            .IsIn(_options.AutoValidateIgnoredHttpMethods);
    }
}
