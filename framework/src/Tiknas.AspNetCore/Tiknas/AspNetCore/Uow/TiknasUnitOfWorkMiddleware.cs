using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Endpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Tiknas.AspNetCore.Middleware;
using Tiknas.DependencyInjection;
using Tiknas.Uow;

namespace Tiknas.AspNetCore.Uow;

public class TiknasUnitOfWorkMiddleware : TiknasMiddlewareBase, ITransientDependency
{
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly TiknasAspNetCoreUnitOfWorkOptions _options;

    public TiknasUnitOfWorkMiddleware(
        IUnitOfWorkManager unitOfWorkManager,
        IOptions<TiknasAspNetCoreUnitOfWorkOptions> options)
    {
        _unitOfWorkManager = unitOfWorkManager;
        _options = options.Value;
    }

    public async override Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (await ShouldSkipAsync(context, next) || IsIgnoredUrl(context))
        {
            await next(context);
            return;
        }

        using (var uow = _unitOfWorkManager.Reserve(UnitOfWork.UnitOfWorkReservationName))
        {
            await next(context);
            await uow.CompleteAsync(context.RequestAborted);
        }
    }

    private bool IsIgnoredUrl(HttpContext context)
    {
        return context.Request.Path.Value != null &&
               _options.IgnoredUrls.Any(x => context.Request.Path.Value.StartsWith(x, StringComparison.OrdinalIgnoreCase));
    }

    protected async override Task<bool> ShouldSkipAsync(HttpContext context, RequestDelegate next)
    {
        // Blazor components will render concurrently, so we need to skip the middleware for them.
        // Otherwise, We will get the following exception:
        // A second operation started on this context before a previous operation completed.
        // This is usually caused by different threads using the same instance of DbContext.
        if (context.GetEndpoint()?.Metadata?.GetMetadata<RootComponentMetadata>() != null)
        {
            return true;
        }

        return await base.ShouldSkipAsync(context, next);
    }
}
