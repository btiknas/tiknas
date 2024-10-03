using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.Json.Newtonsoft;
using Tiknas.Modularity;

namespace Tiknas.AspNetCore.Mvc.NewtonsoftJson;

[DependsOn(typeof(TiknasJsonNewtonsoftModule), typeof(TiknasAspNetCoreMvcModule))]
public class TiknasAspNetCoreMvcNewtonsoftModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMvcCore().AddNewtonsoftJson();

        context.Services.AddOptions<MvcNewtonsoftJsonOptions>()
            .Configure<IServiceProvider>((options, rootServiceProvider) =>
            {
                options.SerializerSettings.ContractResolver = new TiknasCamelCasePropertyNamesContractResolver(rootServiceProvider.GetRequiredService<TiknasDateTimeConverter>());
            });
    }
}
