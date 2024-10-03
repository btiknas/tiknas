using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RequestLocalization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Tiknas.AspNetCore.Auditing;
using Tiknas.AspNetCore.VirtualFileSystem;
using Tiknas.Auditing;
using Tiknas.Authorization;
using Tiknas.ExceptionHandling;
using Tiknas.Http;
using Tiknas.Modularity;
using Tiknas.Security;
using Tiknas.Uow;
using Tiknas.Validation;
using Tiknas.VirtualFileSystem;

namespace Tiknas.AspNetCore;

[DependsOn(
    typeof(TiknasAuditingModule),
    typeof(TiknasSecurityModule),
    typeof(TiknasVirtualFileSystemModule),
    typeof(TiknasUnitOfWorkModule),
    typeof(TiknasHttpModule),
    typeof(TiknasAuthorizationModule),
    typeof(TiknasValidationModule),
    typeof(TiknasExceptionHandlingModule),
    typeof(TiknasAspNetCoreAbstractionsModule)
    )]
public class TiknasAspNetCoreModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var tiknasHostEnvironment = context.Services.GetSingletonInstance<ITiknasHostEnvironment>();
        if (tiknasHostEnvironment.EnvironmentName.IsNullOrWhiteSpace())
        {
            tiknasHostEnvironment.EnvironmentName = context.Services.GetHostingEnvironment().EnvironmentName;
        }
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAuthorization();

        Configure<TiknasAuditingOptions>(options =>
        {
            options.Contributors.Add(new AspNetCoreAuditLogContributor());
        });

        Configure<StaticFileOptions>(options =>
        {
            options.ContentTypeProvider = context.Services.GetRequiredService<TiknasFileExtensionContentTypeProvider>();
        });

        AddAspNetServices(context.Services);
        context.Services.AddObjectAccessor<IApplicationBuilder>();
        context.Services.AddTiknasDynamicOptions<RequestLocalizationOptions, TiknasRequestLocalizationOptionsManager>();
    }

    private static void AddAspNetServices(IServiceCollection services)
    {
        services.AddHttpContextAccessor();
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var environment = context.GetEnvironmentOrNull();
        if (environment != null)
        {
            environment.WebRootFileProvider =
                new CompositeFileProvider(
                    context.GetEnvironment().WebRootFileProvider,
                    context.ServiceProvider.GetRequiredService<IWebContentFileProvider>()
                );
        }
    }
}
