using System;
using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.RequestLocalization;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.StaticAssets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Tiknas;
using Tiknas.AspNetCore.Auditing;
using Tiknas.AspNetCore.ExceptionHandling;
using Tiknas.AspNetCore.Security;
using Tiknas.AspNetCore.Security.Claims;
using Tiknas.AspNetCore.Tracing;
using Tiknas.AspNetCore.Uow;
using Tiknas.AspNetCore.VirtualFileSystem;
using Tiknas.DependencyInjection;
using Tiknas.Threading;
using Tiknas.VirtualFileSystem;

namespace Microsoft.AspNetCore.Builder;

public static class TiknasApplicationBuilderExtensions
{
    private const string ExceptionHandlingMiddlewareMarker = "_TiknasExceptionHandlingMiddleware_Added";

    public async static Task InitializeApplicationAsync([NotNull] this IApplicationBuilder app)
    {
        Check.NotNull(app, nameof(app));

        app.ApplicationServices.GetRequiredService<ObjectAccessor<IApplicationBuilder>>().Value = app;
        var application = app.ApplicationServices.GetRequiredService<ITiknasApplicationWithExternalServiceProvider>();
        var applicationLifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

        applicationLifetime.ApplicationStopping.Register(() =>
        {
            AsyncHelper.RunSync(() => application.ShutdownAsync());
        });

        applicationLifetime.ApplicationStopped.Register(() =>
        {
            application.Dispose();
        });

        await application.InitializeAsync(app.ApplicationServices);
    }

    public static void InitializeApplication([NotNull] this IApplicationBuilder app)
    {
        Check.NotNull(app, nameof(app));

        app.ApplicationServices.GetRequiredService<ObjectAccessor<IApplicationBuilder>>().Value = app;
        var application = app.ApplicationServices.GetRequiredService<ITiknasApplicationWithExternalServiceProvider>();
        var applicationLifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

        applicationLifetime.ApplicationStopping.Register(() =>
        {
            application.Shutdown();
        });

        applicationLifetime.ApplicationStopped.Register(() =>
        {
            application.Dispose();
        });

        application.Initialize(app.ApplicationServices);
    }

    public static IApplicationBuilder UseAuditing(this IApplicationBuilder app)
    {
        return app
            .UseMiddleware<TiknasAuditingMiddleware>();
    }

    public static IApplicationBuilder UseUnitOfWork(this IApplicationBuilder app)
    {
        return app
            .UseTiknasExceptionHandling()
            .UseMiddleware<TiknasUnitOfWorkMiddleware>();
    }

    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
    {
        return app
            .UseMiddleware<TiknasCorrelationIdMiddleware>();
    }

    public static IApplicationBuilder UseTiknasRequestLocalization(this IApplicationBuilder app,
        Action<RequestLocalizationOptions>? optionsAction = null)
    {
        app.ApplicationServices
            .GetRequiredService<ITiknasRequestLocalizationOptionsProvider>()
            .InitLocalizationOptions(optionsAction);

        return app.UseMiddleware<TiknasRequestLocalizationMiddleware>();
    }

    public static IApplicationBuilder UseTiknasExceptionHandling(this IApplicationBuilder app)
    {
        if (app.Properties.ContainsKey(ExceptionHandlingMiddlewareMarker))
        {
            return app;
        }

        app.Properties[ExceptionHandlingMiddlewareMarker] = true;
        return app.UseMiddleware<TiknasExceptionHandlingMiddleware>();
    }

    [Obsolete("Replace with TiknasClaimsTransformation")]
    public static IApplicationBuilder TiknasClaimsMap(this IApplicationBuilder app)
    {
        return app.UseMiddleware<TiknasClaimsMapMiddleware>();
    }

    public static IApplicationBuilder UseTiknasSecurityHeaders(this IApplicationBuilder app)
    {
        return app.UseMiddleware<TiknasSecurityHeadersMiddleware>();
    }

    public static IApplicationBuilder UseDynamicClaims(this IApplicationBuilder app)
    {
        return app.UseMiddleware<TiknasDynamicClaimsMiddleware>();
    }

    /// <summary>
    /// MapTiknasStaticAssets is used to serve the files from the tiknas virtual file system embedded resources(js/css) and call the MapStaticAssets.
    /// </summary>
    public static StaticAssetsEndpointConventionBuilder MapTiknasStaticAssets(this WebApplication app, string? staticAssetsManifestPath = null)
    {
        return app.As<IApplicationBuilder>().MapTiknasStaticAssets(staticAssetsManifestPath);
    }

    /// <summary>
    /// MapTiknasStaticAssets is used to serve the files from the tiknas virtual file system embedded resources(js/css) and call the MapStaticAssets.
    /// </summary>
    public static StaticAssetsEndpointConventionBuilder MapTiknasStaticAssets(this IApplicationBuilder app, string? staticAssetsManifestPath = null)
    {
        if (app is not IEndpointRouteBuilder endpoints)
        {
            throw new TiknasException("The app(IApplicationBuilder) is not an IEndpointRouteBuilder.");
        }

        app.UseVirtualStaticFiles();

        var options = app.ApplicationServices.GetRequiredService<IOptions<TiknasAspNetCoreContentOptions>>().Value;
        foreach (var folder in options.AllowedExtraWebContentFolders)
        {
            app.UseVirtualStaticFiles(folder);
        }

        return endpoints.MapStaticAssets(staticAssetsManifestPath);
    }

    /// <summary>
    /// This static file provider is used to serve the files from the tiknas virtual file system embedded resources(js/css).
    /// It will not serve the files from the application's wwwroot folder.
    /// </summary>
    public static IApplicationBuilder UseVirtualStaticFiles(this IApplicationBuilder app)
    {
        app.UseStaticFiles(new StaticFileOptions()
        {
            ContentTypeProvider = app.ApplicationServices.GetRequiredService<TiknasFileExtensionContentTypeProvider>(),
            FileProvider = new WebContentFileProvider(
                app.ApplicationServices.GetRequiredService<IVirtualFileProvider>(),
                new EmptyHostingEnvironment(),
                app.ApplicationServices.GetRequiredService<IOptions<TiknasAspNetCoreContentOptions>>()
            )
        });

        return app;
    }

    /// <summary>
    /// This static file provider is used to serve the files from the <param name="app"></param> in the <param name="folder">folder</param>.
    /// </summary>
    public static IApplicationBuilder UseVirtualStaticFiles(this IApplicationBuilder app, string folder)
    {
        folder = folder.TrimStart('/').TrimEnd('/');

        var root = Path.Combine(app.ApplicationServices.GetRequiredService<IWebHostEnvironment>().ContentRootPath, folder);
        if (!Directory.Exists(root))
        {
            return app;
        }

        app.UseStaticFiles(new StaticFileOptions
        {
            ContentTypeProvider = app.ApplicationServices.GetRequiredService<TiknasFileExtensionContentTypeProvider>(),
            FileProvider = new PhysicalFileProvider(root),
            RequestPath = $"/{folder}"
        });

        return app;
    }
}
