using System;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.Modularity;
using Tiknas.Timing;

namespace Tiknas.Json.Newtonsoft;

[DependsOn(typeof(TiknasJsonAbstractionsModule), typeof(TiknasTimingModule))]
public class TiknasJsonNewtonsoftModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddOptions<TiknasNewtonsoftJsonSerializerOptions>()
            .Configure<IServiceProvider>((options, rootServiceProvider) =>
            {
                options.JsonSerializerSettings.ContractResolver = new TiknasCamelCasePropertyNamesContractResolver(rootServiceProvider.GetRequiredService<TiknasDateTimeConverter>());
            });
    }
}
