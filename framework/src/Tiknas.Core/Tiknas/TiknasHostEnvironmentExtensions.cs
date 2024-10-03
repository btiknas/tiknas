using System;
using Microsoft.Extensions.Hosting;

namespace Tiknas;

public static class TiknasHostEnvironmentExtensions
{
    public static bool IsDevelopment(this ITiknasHostEnvironment hostEnvironment)
    {
        Check.NotNull(hostEnvironment, nameof(hostEnvironment));

        return hostEnvironment.IsEnvironment(Environments.Development);
    }

    public static bool IsStaging(this ITiknasHostEnvironment hostEnvironment)
    {
        Check.NotNull(hostEnvironment, nameof(hostEnvironment));

        return hostEnvironment.IsEnvironment(Environments.Staging);
    }

    public static bool IsProduction(this ITiknasHostEnvironment hostEnvironment)
    {
        Check.NotNull(hostEnvironment, nameof(hostEnvironment));

        return hostEnvironment.IsEnvironment(Environments.Production);
    }

    public static bool IsEnvironment(this ITiknasHostEnvironment hostEnvironment, string environmentName)
    {
        Check.NotNull(hostEnvironment, nameof(hostEnvironment));

        return string.Equals(
            hostEnvironment.EnvironmentName,
            environmentName,
            StringComparison.OrdinalIgnoreCase);
    }
}
