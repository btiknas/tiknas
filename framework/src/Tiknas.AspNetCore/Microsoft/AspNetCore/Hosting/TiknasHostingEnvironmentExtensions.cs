using System;
using Microsoft.Extensions.Configuration;

namespace Microsoft.AspNetCore.Hosting;

public static class TiknasHostingEnvironmentExtensions
{
    public static IConfigurationRoot BuildConfiguration(
        this IWebHostEnvironment env,
        TiknasConfigurationBuilderOptions? options = null)
    {
        options ??= new TiknasConfigurationBuilderOptions();

        if (options.BasePath.IsNullOrEmpty())
        {
            options.BasePath = env.ContentRootPath;
        }

        if (options.EnvironmentName.IsNullOrEmpty())
        {
            options.EnvironmentName = env.EnvironmentName;
        }

        return ConfigurationHelper.BuildConfiguration(options);
    }
}
