using System;
using System.Linq;
using Asp.Versioning;
using Asp.Versioning.ApplicationModels;
using Tiknas.ApiVersioning;
using Tiknas.AspNetCore.Mvc;
using Tiknas.AspNetCore.Mvc.Conventions;
using Tiknas.AspNetCore.Mvc.Versioning;

namespace Microsoft.Extensions.DependencyInjection;

public static class TiknasApiVersioningExtensions
{
    public static IApiVersioningBuilder AddTiknasApiVersioning(
        this IServiceCollection services,
        Action<ApiVersioningOptions>? apiVersioningOptionsSetupAction = null,
        Action<MvcApiVersioningOptions>? mvcApiVersioningOptionsSetupAction = null)
    {
        services.AddTransient<IRequestedApiVersion, HttpContextRequestedApiVersion>();
        services.AddTransient<IApiControllerSpecification, TiknasConventionalApiControllerSpecification>();

        apiVersioningOptionsSetupAction ??= _ => { };
        mvcApiVersioningOptionsSetupAction ??= _ => { };
        return services.AddApiVersioning(apiVersioningOptionsSetupAction).AddMvc(mvcApiVersioningOptionsSetupAction);
    }

    public static void ConfigureTiknas(this MvcApiVersioningOptions options, TiknasAspNetCoreMvcOptions mvcOptions)
    {
        foreach (var setting in mvcOptions.ConventionalControllers.ConventionalControllerSettings)
        {
            if (setting.MvcApiVersioningConfigurer == null)
            {
                ConfigureApiVersionsByConvention(options, setting);
            }
            else
            {
                setting.MvcApiVersioningConfigurer.Invoke(options);
            }
        }
    }

    private static void ConfigureApiVersionsByConvention(MvcApiVersioningOptions options, ConventionalControllerSetting setting)
    {
        foreach (var controllerType in setting.ControllerTypes)
        {
            var controllerBuilder = options.Conventions.Controller(controllerType);

            if (setting.ApiVersions.Any())
            {
                foreach (var apiVersion in setting.ApiVersions)
                {
                    controllerBuilder.HasApiVersion(apiVersion);
                }
            }
            else
            {
                if (!controllerType.IsDefined(typeof(ApiVersionAttribute), true))
                {
                    controllerBuilder.IsApiVersionNeutral();
                }
            }
        }
    }
}
