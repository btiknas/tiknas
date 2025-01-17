using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Tiknas.AspNetCore.Middleware;
using Tiknas.Auditing;
using Tiknas.DependencyInjection;
using Tiknas.Uow;
using Tiknas.Users;

namespace Tiknas.AspNetCore.Auditing;

public class TiknasAuditingMiddleware : TiknasMiddlewareBase, ITransientDependency
{
    private readonly IAuditingManager _auditingManager;
    protected TiknasAuditingOptions AuditingOptions { get; }
    protected TiknasAspNetCoreAuditingOptions AspNetCoreAuditingOptions { get; }
    protected ICurrentUser CurrentUser { get; }
    protected IUnitOfWorkManager UnitOfWorkManager { get; }

    public TiknasAuditingMiddleware(
        IAuditingManager auditingManager,
        ICurrentUser currentUser,
        IOptions<TiknasAuditingOptions> auditingOptions,
        IOptions<TiknasAspNetCoreAuditingOptions> aspNetCoreAuditingOptions,
        IUnitOfWorkManager unitOfWorkManager)
    {
        _auditingManager = auditingManager;

        CurrentUser = currentUser;
        UnitOfWorkManager = unitOfWorkManager;
        AuditingOptions = auditingOptions.Value;
        AspNetCoreAuditingOptions = aspNetCoreAuditingOptions.Value;
    }

    public async override Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (await ShouldSkipAsync(context, next) || !AuditingOptions.IsEnabled || IsIgnoredUrl(context))
        {
            await next(context);
            return;
        }

        var hasError = false;
        using (var saveHandle = _auditingManager.BeginScope())
        {
            Debug.Assert(_auditingManager.Current != null);

            try
            {
                await next(context);

                if (_auditingManager.Current.Log.Exceptions.Any())
                {
                    hasError = true;
                }
            }
            catch (Exception ex)
            {
                hasError = true;

                if (!_auditingManager.Current.Log.Exceptions.Contains(ex))
                {
                    _auditingManager.Current.Log.Exceptions.Add(ex);
                }

                throw;
            }
            finally
            {
                if (await ShouldWriteAuditLogAsync(_auditingManager.Current.Log, context, hasError))
                {
                    if (UnitOfWorkManager.Current != null)
                    {
                        try
                        {
                            await UnitOfWorkManager.Current.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {
                            if (!_auditingManager.Current.Log.Exceptions.Contains(ex))
                            {
                                _auditingManager.Current.Log.Exceptions.Add(ex);
                            }
                        }
                    }

                    await saveHandle.SaveAsync();
                }
            }
        }
    }

    private bool IsIgnoredUrl(HttpContext context)
    {
        if (context.Request.Path.Value == null)
        {
            return false;
        }

        if (!AuditingOptions.IsEnabledForIntegrationServices &&
            context.Request.Path.Value.StartsWith($"/{TiknasAspNetCoreConsts.DefaultIntegrationServiceApiPrefix}/", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (AspNetCoreAuditingOptions.IgnoredUrls.Any(x => context.Request.Path.Value.StartsWith(x, StringComparison.OrdinalIgnoreCase)))
        {
            return true;
        }

        return false;
    }

    private async Task<bool> ShouldWriteAuditLogAsync(AuditLogInfo auditLogInfo, HttpContext httpContext, bool hasError)
    {
        foreach (var selector in AuditingOptions.AlwaysLogSelectors)
        {
            if (await selector(auditLogInfo))
            {
                return true;
            }
        }

        if (AuditingOptions.AlwaysLogOnException && hasError)
        {
            return true;
        }

        if (!AuditingOptions.IsEnabledForAnonymousUsers && !CurrentUser.IsAuthenticated)
        {
            return false;
        }

        if (!AuditingOptions.IsEnabledForGetRequests &&
            (string.Equals(httpContext.Request.Method, HttpMethods.Get, StringComparison.OrdinalIgnoreCase) ||
             string.Equals(httpContext.Request.Method, HttpMethods.Head, StringComparison.OrdinalIgnoreCase)))
        {
            return false;
        }

        return true;
    }
}
