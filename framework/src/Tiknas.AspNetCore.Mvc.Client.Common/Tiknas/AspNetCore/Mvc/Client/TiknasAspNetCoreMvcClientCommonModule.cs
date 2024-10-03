using Microsoft.Extensions.DependencyInjection;
using Pages.Tiknas.MultiTenancy.ClientProxies;
using Tiknas.AspNetCore.Mvc.ApplicationConfigurations.ClientProxies;
using Tiknas.Authorization;
using Tiknas.Caching;
using Tiknas.Features;
using Tiknas.Http.Client;
using Tiknas.Localization;
using Tiknas.Modularity;
using Tiknas.Security.Claims;
using Tiknas.VirtualFileSystem;

namespace Tiknas.AspNetCore.Mvc.Client;

[DependsOn(
    typeof(TiknasHttpClientModule),
    typeof(TiknasAspNetCoreMvcContractsModule),
    typeof(TiknasCachingModule),
    typeof(TiknasLocalizationModule),
    typeof(TiknasAuthorizationModule),
    typeof(TiknasFeaturesModule),
    typeof(TiknasVirtualFileSystemModule)
)]
public class TiknasAspNetCoreMvcClientCommonModule : TiknasModule
{
    public const string RemoteServiceName = "TiknasMvcClient";

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddStaticHttpClientProxies(typeof(TiknasAspNetCoreMvcContractsModule).Assembly, RemoteServiceName);

        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TiknasAspNetCoreMvcClientCommonModule>();
        });

        Configure<TiknasLocalizationOptions>(options =>
        {
            options.GlobalContributors.Add<RemoteLocalizationContributor>();
        });

        context.Services.AddTransient<TiknasApplicationConfigurationClientProxy>();
        context.Services.AddTransient<TiknasTenantClientProxy>();

        var tiknasClaimsPrincipalFactoryOptions = context.Services.ExecutePreConfiguredActions<TiknasClaimsPrincipalFactoryOptions>();
        if (tiknasClaimsPrincipalFactoryOptions.IsRemoteRefreshEnabled)
        {
            context.Services.AddTransient<RemoteDynamicClaimsPrincipalContributor>();
            context.Services.AddTransient<RemoteDynamicClaimsPrincipalContributorCache>();
        }
    }
}
