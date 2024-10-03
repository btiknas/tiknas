using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Tiknas.ApiVersioning;
using Tiknas.Application;
using Tiknas.AspNetCore.Mvc.AntiForgery;
using Tiknas.AspNetCore.Mvc.ApiExploring;
using Tiknas.AspNetCore.Mvc.ApplicationModels;
using Tiknas.AspNetCore.Mvc.Conventions;
using Tiknas.AspNetCore.Mvc.DataAnnotations;
using Tiknas.AspNetCore.Mvc.DependencyInjection;
using Tiknas.AspNetCore.Mvc.Infrastructure;
using Tiknas.AspNetCore.Mvc.Json;
using Tiknas.AspNetCore.Mvc.Libs;
using Tiknas.AspNetCore.Mvc.Localization;
using Tiknas.AspNetCore.VirtualFileSystem;
using Tiknas.DependencyInjection;
using Tiknas.Http;
using Tiknas.DynamicProxy;
using Tiknas.GlobalFeatures;
using Tiknas.Http.Modeling;
using Tiknas.Http.ProxyScripting.Generators.JQuery;
using Tiknas.Json;
using Tiknas.Json.SystemTextJson;
using Tiknas.Localization;
using Tiknas.Modularity;
using Tiknas.UI;
using Tiknas.UI.Navigation;
using Tiknas.Validation.Localization;

namespace Tiknas.AspNetCore.Mvc;

[DependsOn(
    typeof(TiknasAspNetCoreModule),
    typeof(TiknasLocalizationModule),
    typeof(TiknasApiVersioningAbstractionsModule),
    typeof(TiknasAspNetCoreMvcContractsModule),
    typeof(TiknasUiNavigationModule),
    typeof(TiknasGlobalFeaturesModule),
    typeof(TiknasDddApplicationModule),
    typeof(TiknasJsonSystemTextJsonModule)
    )]
public class TiknasAspNetCoreMvcModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        DynamicProxyIgnoreTypes.Add<ControllerBase>();
        DynamicProxyIgnoreTypes.Add<PageModel>();
        DynamicProxyIgnoreTypes.Add<ViewComponent>();

        context.Services.AddConventionalRegistrar(new TiknasAspNetCoreMvcConventionalRegistrar());
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasApiDescriptionModelOptions>(options =>
        {
            options.IgnoredInterfaces.AddIfNotContains(typeof(IAsyncActionFilter));
            options.IgnoredInterfaces.AddIfNotContains(typeof(IFilterMetadata));
            options.IgnoredInterfaces.AddIfNotContains(typeof(IActionFilter));
        });

        Configure<TiknasRemoteServiceApiDescriptionProviderOptions>(options =>
        {
            var statusCodes = new List<int>
            {
                (int) HttpStatusCode.Forbidden,
                (int) HttpStatusCode.Unauthorized,
                (int) HttpStatusCode.BadRequest,
                (int) HttpStatusCode.NotFound,
                (int) HttpStatusCode.NotImplemented,
                (int) HttpStatusCode.InternalServerError
            };

            options.SupportedResponseTypes.AddIfNotContains(statusCodes.Select(statusCode => new ApiResponseType
            {
                Type = typeof(RemoteServiceErrorResponse),
                StatusCode = statusCode
            }));
        });

        context.Services.PostConfigure<TiknasAspNetCoreMvcOptions>(options =>
        {
            if (options.MinifyGeneratedScript == null)
            {
                options.MinifyGeneratedScript = context.Services.GetHostingEnvironment().IsProduction();
            }
        });

        var mvcCoreBuilder = context.Services.AddMvcCore(options =>
        {
            options.Filters.Add(new TiknasAutoValidateAntiforgeryTokenAttribute());
        });
        context.Services.ExecutePreConfiguredActions(mvcCoreBuilder);

        var tiknasMvcDataAnnotationsLocalizationOptions = context.Services
            .ExecutePreConfiguredActions(
                new TiknasMvcDataAnnotationsLocalizationOptions()
            );

        context.Services
            .AddSingleton<IOptions<TiknasMvcDataAnnotationsLocalizationOptions>>(
                new OptionsWrapper<TiknasMvcDataAnnotationsLocalizationOptions>(
                    tiknasMvcDataAnnotationsLocalizationOptions
                )
            );

        var mvcBuilder = context.Services.AddMvc()
            .AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                {
                    var resourceType = tiknasMvcDataAnnotationsLocalizationOptions
                        .AssemblyResources
                        .GetOrDefault(type.Assembly);

                    if (resourceType != null)
                    {
                        return factory.Create(resourceType);
                    }

                    return factory.CreateDefaultOrNull() ??
                            factory.Create(type);
                };
            })
            .AddViewLocalization(); //TODO: How to configure from the application? Also, consider to move to a UI module since APIs does not care about it.

        if (context.Services.GetHostingEnvironment().IsDevelopment() &&
            context.Services.ExecutePreConfiguredActions<TiknasAspNetCoreMvcOptions>().EnableRazorRuntimeCompilationOnDevelopment)
        {
            mvcCoreBuilder.AddTiknasRazorRuntimeCompilation();
        }

        mvcCoreBuilder.AddTiknasJson();

        context.Services.ExecutePreConfiguredActions(mvcBuilder);

        //TODO: AddViewLocalization by default..?

        context.Services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

        //Use DI to create controllers
        mvcBuilder.AddControllersAsServices();

        //Use DI to create view components
        mvcBuilder.AddViewComponentsAsServices();

        //Use DI to create razor page
        context.Services.Replace(ServiceDescriptor.Singleton<IPageModelActivatorProvider, ServiceBasedPageModelActivatorProvider>());

        //Add feature providers
        var partManager = context.Services.GetSingletonInstance<ApplicationPartManager>();
        var application = context.Services.GetSingletonInstance<ITiknasApplication>();

        partManager.FeatureProviders.Add(new TiknasConventionalControllerFeatureProvider(application));
        partManager.ApplicationParts.AddIfNotContains(typeof(TiknasAspNetCoreMvcModule).Assembly);

        context.Services.Replace(ServiceDescriptor.Singleton<IValidationAttributeAdapterProvider, TiknasValidationAttributeAdapterProvider>());
        context.Services.AddSingleton<ValidationAttributeAdapterProvider>();

        context.Services.TryAddEnumerable(ServiceDescriptor.Transient<IActionDescriptorProvider, TiknasMvcActionDescriptorProvider>());
        context.Services.AddOptions<MvcOptions>()
            .Configure<IServiceProvider>((mvcOptions, serviceProvider) =>
            {
                mvcOptions.AddTiknas(context.Services);

                // serviceProvider is root service provider.
                var stringLocalizer = serviceProvider.GetRequiredService<IStringLocalizer<TiknasValidationResource>>();
                mvcOptions.ModelBindingMessageProvider.SetValueIsInvalidAccessor(_ => stringLocalizer["The value '{0}' is invalid."]);
                mvcOptions.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => stringLocalizer["The field must be a number."]);
                mvcOptions.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(value => stringLocalizer["The field {0} must be a number.", value]);
            });

        Configure<TiknasEndpointRouterOptions>(options =>
        {
            options.EndpointConfigureActions.Add(endpointContext =>
            {
                endpointContext.Endpoints.MapControllerRoute("defaultWithArea", "{area}/{controller=Home}/{action=Index}/{id?}");
                endpointContext.Endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpointContext.Endpoints.MapRazorPages();
            });
        });

        Configure<DynamicJavaScriptProxyOptions>(options =>
        {
            options.DisableModule("tiknas");
        });

        context.Services.Replace(ServiceDescriptor.Singleton<IHttpResponseStreamWriterFactory, TiknasMemoryPoolHttpResponseStreamWriterFactory>());
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        ApplicationPartSorter.Sort(
            context.Services.GetSingletonInstance<ApplicationPartManager>(),
            context.Services.GetSingletonInstance<IModuleContainer>()
        );

        var preConfigureActions = context.Services.GetPreConfigureActions<TiknasAspNetCoreMvcOptions>();

        DynamicProxyIgnoreTypes.Add(preConfigureActions.Configure()
            .ConventionalControllers
            .ConventionalControllerSettings.SelectMany(x => x.ControllerTypes).ToArray());

        Configure<TiknasAspNetCoreMvcOptions>(options =>
        {
            preConfigureActions.Configure(options);
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        AddApplicationParts(context);
        CheckLibs(context);
    }

    private static void AddApplicationParts(ApplicationInitializationContext context)
    {
        var partManager = context.ServiceProvider.GetService<ApplicationPartManager>();
        if (partManager == null)
        {
            return;
        }

        var moduleContainer = context.ServiceProvider.GetRequiredService<IModuleContainer>();

        var plugInModuleAssemblies = moduleContainer
            .Modules
            .Where(m => m.IsLoadedAsPlugIn)
            .SelectMany(m => m.AllAssemblies)
            .Distinct();

        AddToApplicationParts(partManager, plugInModuleAssemblies);

        var controllerAssemblies = context
            .ServiceProvider
            .GetRequiredService<IOptions<TiknasAspNetCoreMvcOptions>>()
            .Value
            .ConventionalControllers
            .ConventionalControllerSettings
            .Select(s => s.Assembly)
            .Distinct();

        AddToApplicationParts(partManager, controllerAssemblies);

        var additionalAssemblies = moduleContainer
            .Modules
            .SelectMany(m => m.GetAdditionalAssemblies())
            .Distinct();

        AddToApplicationParts(partManager, additionalAssemblies);
    }

    private static void AddToApplicationParts(ApplicationPartManager partManager, IEnumerable<Assembly> moduleAssemblies)
    {
        foreach (var moduleAssembly in moduleAssemblies)
        {
            partManager.ApplicationParts.AddIfNotContains(moduleAssembly);
        }
    }

    private static void CheckLibs(ApplicationInitializationContext context)
    {
        context.ServiceProvider.GetRequiredService<ITiknasMvcLibsService>().CheckLibs(context);
    }
}
