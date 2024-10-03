using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tiknas.Authorization.Localization;
using Tiknas.Authorization.Permissions;
using Tiknas.Localization;
using Tiknas.Localization.ExceptionHandling;
using Tiknas.Modularity;
using Tiknas.MultiTenancy;
using Tiknas.Security;
using Tiknas.VirtualFileSystem;

namespace Tiknas.Authorization;

[DependsOn(
    typeof(TiknasAuthorizationAbstractionsModule),
    typeof(TiknasSecurityModule),
    typeof(TiknasLocalizationModule),
    typeof(TiknasMultiTenancyModule)
)]
public class TiknasAuthorizationModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.OnRegistered(AuthorizationInterceptorRegistrar.RegisterIfNeeded);
        AutoAddDefinitionProviders(context.Services);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAuthorizationCore();

        context.Services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
        context.Services.AddSingleton<IAuthorizationHandler, PermissionsRequirementHandler>();

        context.Services.TryAddTransient<DefaultAuthorizationPolicyProvider>();

        Configure<TiknasPermissionOptions>(options =>
        {
            options.ValueProviders.Add<UserPermissionValueProvider>();
            options.ValueProviders.Add<RolePermissionValueProvider>();
            options.ValueProviders.Add<ClientPermissionValueProvider>();
        });

        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TiknasAuthorizationResource>();
        });

        Configure<TiknasLocalizationOptions>(options =>
        {
            options.Resources
                .Add<TiknasAuthorizationResource>("en")
                .AddVirtualJson("/Tiknas/Authorization/Localization");
        });

        Configure<TiknasExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("Tiknas.Authorization", typeof(TiknasAuthorizationResource));
        });
    }

    private static void AutoAddDefinitionProviders(IServiceCollection services)
    {
        var definitionProviders = new List<Type>();

        services.OnRegistered(context =>
        {
            if (typeof(IPermissionDefinitionProvider).IsAssignableFrom(context.ImplementationType))
            {
                definitionProviders.Add(context.ImplementationType);
            }
        });

        services.Configure<TiknasPermissionOptions>(options =>
        {
            options.DefinitionProviders.AddIfNotContains(definitionProviders);
        });
    }
}
