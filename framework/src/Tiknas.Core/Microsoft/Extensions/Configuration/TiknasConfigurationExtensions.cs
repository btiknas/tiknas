using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.Configuration;

public static class TiknasConfigurationExtensions
{
    public static IConfigurationBuilder AddAppSettingsSecretsJson(
        this IConfigurationBuilder builder,
        bool optional = true,
        bool reloadOnChange = true,
        string path = TiknasHostingHostBuilderExtensions.AppSettingsSecretJsonPath)
    {
        return builder.AddJsonFile(path: path, optional: optional, reloadOnChange: reloadOnChange);
    }
}
