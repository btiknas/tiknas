using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tiknas.Http.Client;
using Tiknas.Json;
using Tiknas.Modularity;
using Tiknas.MultiTenancy;
using Tiknas.RemoteServices;

namespace Tiknas.Dapr;

[DependsOn(
    typeof(TiknasJsonModule),
    typeof(TiknasMultiTenancyAbstractionsModule),
    typeof(TiknasHttpClientModule)
)]
public class TiknasDaprModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        ConfigureDaprOptions(configuration);
    }

    private void ConfigureDaprOptions(IConfiguration configuration)
    {
        Configure<TiknasDaprOptions>(configuration.GetSection("Dapr"));
        Configure<TiknasDaprOptions>(options =>
        {
            if (options.DaprApiToken.IsNullOrWhiteSpace())
            {
                var confEnv = configuration["DAPR_API_TOKEN"];
                if (!confEnv.IsNullOrWhiteSpace())
                {
                    options.DaprApiToken = confEnv!;
                }
                else
                {
                    var env = Environment.GetEnvironmentVariable("DAPR_API_TOKEN");
                    if (!env.IsNullOrWhiteSpace())
                    {
                        options.DaprApiToken = env!;
                    }
                }
            }

            if (options.AppApiToken.IsNullOrWhiteSpace())
            {
                var confEnv = configuration["APP_API_TOKEN"];
                if (!confEnv.IsNullOrWhiteSpace())
                {
                    options.AppApiToken = confEnv!;
                }
                else
                {
                    var env = Environment.GetEnvironmentVariable("APP_API_TOKEN");
                    if (!env.IsNullOrWhiteSpace())
                    {
                        options.AppApiToken = env!;
                    }
                }
            }
        });
    }
}
