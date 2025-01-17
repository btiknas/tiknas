﻿using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.Hosting;

public static class TiknasHostingHostBuilderExtensions
{
    public const string AppSettingsSecretJsonPath = "appsettings.secrets.json";

    public static IHostBuilder AddAppSettingsSecretsJson(
        this IHostBuilder hostBuilder,
        bool optional = true,
        bool reloadOnChange = true,
        string path = AppSettingsSecretJsonPath)
    {
        return hostBuilder.ConfigureAppConfiguration((_, builder) =>
        {
            builder.AddJsonFile(
                path: path,
                optional: optional,
                reloadOnChange: reloadOnChange
            );
        });
    }
}
