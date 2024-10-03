using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.AspNetCore.Mvc;
using Tiknas.Http.Client;
using Tiknas.Http.Client.ClientProxying;
using Tiknas.Http.DynamicProxying;
using Tiknas.Http.Localization;
using Tiknas.Localization;
using Tiknas.Localization.ExceptionHandling;
using Tiknas.Modularity;
using Tiknas.TestApp;
using Tiknas.TestApp.Application.Dto;
using Tiknas.VirtualFileSystem;

namespace Tiknas.Http;

[DependsOn(
    typeof(TiknasHttpClientModule),
    typeof(TiknasAspNetCoreMvcTestModule)
    )]
public class TiknasHttpClientTestModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(typeof(TestAppModule).Assembly);
        context.Services.AddHttpClientProxy<IRegularTestController>();

        Configure<TiknasRemoteServiceOptions>(options =>
        {
            options.RemoteServices.Default = new RemoteServiceConfiguration("/");
        });


        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TiknasHttpClientTestModule>();
        });

        Configure<TiknasLocalizationOptions>(options =>
        {
            options.Resources
                .Add<HttpClientTestResource>("en")
                .AddVirtualJson("/Tiknas/Http/Localization");
        });

        Configure<TiknasExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("Tiknas.Http.DynamicProxying", typeof(HttpClientTestResource));
        });

        Configure<TiknasAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.FormBodyBindingIgnoredTypes.Add(typeof(CreateFileInput));
            options.ConventionalControllers.FormBodyBindingIgnoredTypes.Add(typeof(CreateMultipleFileInput));
        });

        Configure<TiknasHttpClientProxyingOptions>(options =>
        {
            options.QueryStringConverts.Add(typeof(List<GetParamsNameValue>), typeof(TestObjectToQueryString));
            options.FormDataConverts.Add(typeof(List<GetParamsNameValue>), typeof(TestObjectToFormData));
            options.PathConverts.Add(typeof(int), typeof(TestObjectToPath));
        });
    }
}
