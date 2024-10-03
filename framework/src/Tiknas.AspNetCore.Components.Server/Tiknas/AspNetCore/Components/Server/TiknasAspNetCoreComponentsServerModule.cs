using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Tiknas.AspNetCore.Auditing;
using Tiknas.AspNetCore.Components.Server.Extensibility;
using Tiknas.AspNetCore.Components.Web;
using Tiknas.AspNetCore.Mvc;
using Tiknas.AspNetCore.SignalR;
using Tiknas.AspNetCore.Uow;
using Tiknas.EventBus;
using Tiknas.Http.Client;
using Tiknas.Modularity;

namespace Tiknas.AspNetCore.Components.Server;

[DependsOn(
    typeof(TiknasHttpClientModule),
    typeof(TiknasAspNetCoreComponentsWebModule),
    typeof(TiknasAspNetCoreSignalRModule),
    typeof(TiknasEventBusModule),
    typeof(TiknasAspNetCoreMvcContractsModule)
    )]
public class TiknasAspNetCoreComponentsServerModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        StaticWebAssetsLoader.UseStaticWebAssets(context.Services.GetHostingEnvironment(), context.Services.GetConfiguration());
        context.Services.AddHttpClient(nameof(BlazorServerLookupApiRequestService))
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.All
            });
        var serverSideBlazorBuilder = context.Services.AddServerSideBlazor(options =>
        {
            if (context.Services.GetHostingEnvironment().IsDevelopment())
            {
                options.DetailedErrors = true;
            }
        });
        context.Services.ExecutePreConfiguredActions(serverSideBlazorBuilder);

        Configure<TiknasAspNetCoreUnitOfWorkOptions>(options =>
        {
            options.IgnoredUrls.AddIfNotContains("/_blazor");
        });

        Configure<TiknasAspNetCoreAuditingOptions>(options =>
        {
            options.IgnoredUrls.AddIfNotContains("/_blazor");
        });

        if (!context.Services.ExecutePreConfiguredActions<TiknasAspNetCoreComponentsWebOptions>().IsBlazorWebApp)
        {
            var preConfigureActions = context.Services.GetPreConfigureActions<HttpConnectionDispatcherOptions>();
            Configure<TiknasEndpointRouterOptions>(options =>
            {
                options.EndpointConfigureActions.Add(endpointContext =>
                {
                    endpointContext.Endpoints.MapBlazorHub(httpConnectionDispatcherOptions =>
                    {
                        preConfigureActions.Configure(httpConnectionDispatcherOptions);
                    });
                    endpointContext.Endpoints.MapFallbackToPage("/_Host");
                });
            });
        }
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        context.GetEnvironment().WebRootFileProvider =
            new CompositeFileProvider(
                new ManifestEmbeddedFileProvider(typeof(IServerSideBlazorBuilder).Assembly),
                new ManifestEmbeddedFileProvider(typeof(RazorComponentsEndpointRouteBuilderExtensions).Assembly),
                context.GetEnvironment().WebRootFileProvider
            );
    }
}
