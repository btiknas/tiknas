using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Tiknas.AspNetCore.Middleware;

public abstract class TiknasMiddlewareBase : IMiddleware
{
    protected virtual Task<bool> ShouldSkipAsync(HttpContext context, RequestDelegate next)
    {
        var endpoint = context.GetEndpoint();
        var controllerActionDescriptor = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();
        var disableTiknasFeaturesAttribute = controllerActionDescriptor?.ControllerTypeInfo.GetCustomAttribute<DisableTiknasFeaturesAttribute>();
        return Task.FromResult(disableTiknasFeaturesAttribute != null && disableTiknasFeaturesAttribute.DisableMiddleware);
    }

    public abstract Task InvokeAsync(HttpContext context, RequestDelegate next);
}
