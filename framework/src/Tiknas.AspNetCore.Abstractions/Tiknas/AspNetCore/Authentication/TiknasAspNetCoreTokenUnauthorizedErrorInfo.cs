using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Authentication;

public class TiknasAspNetCoreTokenUnauthorizedErrorInfo : IScopedDependency
{
    public string? Error { get; set; }

    public string? ErrorDescription { get; set; }

    public string? ErrorUri { get; set; }
}
