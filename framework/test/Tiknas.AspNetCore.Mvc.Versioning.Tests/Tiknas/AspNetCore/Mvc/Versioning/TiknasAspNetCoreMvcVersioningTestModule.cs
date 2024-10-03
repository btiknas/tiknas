using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.AspNetCore.TestBase;
using Tiknas.Autofac;
using Tiknas.Http.Client;
using Tiknas.Modularity;

namespace Tiknas.AspNetCore.Mvc.Versioning;

[DependsOn(
    typeof(TiknasAspNetCoreTestBaseModule),
    typeof(TiknasAspNetCoreMvcModule),
    typeof(TiknasAutofacModule),
    typeof(TiknasHttpClientModule)
    )]
public class TiknasAspNetCoreMvcVersioningTestModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<TiknasAspNetCoreMvcOptions>(options =>
        {
                //2.0 Version
                options.ConventionalControllers.Create(typeof(TiknasAspNetCoreMvcVersioningTestModule).Assembly, opts =>
            {
                opts.TypePredicate = t => t.Namespace == typeof(Tiknas.AspNetCore.Mvc.Versioning.App.v2.TodoAppService).Namespace;
                opts.ApiVersions.Add(new ApiVersion(2, 0));
            });

                //1.0 Compatibility version
                options.ConventionalControllers.Create(typeof(TiknasAspNetCoreMvcVersioningTestModule).Assembly, opts =>
            {
                opts.TypePredicate = t => t.Namespace == typeof(Tiknas.AspNetCore.Mvc.Versioning.App.v1.TodoAppService).Namespace;
                opts.ApiVersions.Add(new ApiVersion(1, 0));
            });
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var preActions = context.Services.GetPreConfigureActions<TiknasAspNetCoreMvcOptions>();
        Configure<TiknasAspNetCoreMvcOptions>(options =>
        {
            preActions.Configure(options);
        });

        context.Services.AddTiknasApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;

            //options.ApiVersionReader = new HeaderApiVersionReader("api-version"); //Supports header too
            //options.ApiVersionReader = new MediaTypeApiVersionReader(); //Supports accept header too
        }, options =>
        {
            options.ConfigureTiknas(preActions.Configure());
        });

        context.Services.AddHttpClientProxies(typeof(TiknasAspNetCoreMvcVersioningTestModule).Assembly);

        Configure<TiknasRemoteServiceOptions>(options =>
        {
            options.RemoteServices.Default = new RemoteServiceConfiguration("/");
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        app.UseRouting();
        app.UseConfiguredEndpoints();
    }
}
