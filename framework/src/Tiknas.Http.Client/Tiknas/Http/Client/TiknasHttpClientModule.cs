using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.Castle;
using Tiknas.Data;
using Tiknas.EventBus;
using Tiknas.Modularity;
using Tiknas.MultiTenancy;
using Tiknas.Threading;
using Tiknas.Validation;
using Tiknas.ExceptionHandling;
using Tiknas.Http.Client.ClientProxying;
using Tiknas.Http.Client.ClientProxying.ExtraPropertyDictionaryConverts;
using Tiknas.Http.Client.DynamicProxying;
using Tiknas.RemoteServices;

namespace Tiknas.Http.Client;

[DependsOn(
    typeof(TiknasHttpModule),
    typeof(TiknasCastleCoreModule),
    typeof(TiknasThreadingModule),
    typeof(TiknasMultiTenancyModule),
    typeof(TiknasValidationModule),
    typeof(TiknasExceptionHandlingModule),
    typeof(TiknasRemoteServicesModule),
    typeof(TiknasEventBusModule)
    )]
public class TiknasHttpClientModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClient();
        context.Services.AddTransient(typeof(DynamicHttpProxyInterceptorClientProxy<>));

        Configure<TiknasHttpClientProxyingOptions>(options =>
        {
            options.QueryStringConverts.Add(typeof(ExtraPropertyDictionary), typeof(ExtraPropertyDictionaryToQueryString));
            options.FormDataConverts.Add(typeof(ExtraPropertyDictionary), typeof(ExtraPropertyDictionaryToFormData));
        });
    }
}
