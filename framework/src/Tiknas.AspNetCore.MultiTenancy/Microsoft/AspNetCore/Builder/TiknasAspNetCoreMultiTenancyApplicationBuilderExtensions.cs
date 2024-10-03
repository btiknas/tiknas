using Tiknas.AspNetCore.MultiTenancy;

namespace Microsoft.AspNetCore.Builder;

public static class TiknasAspNetCoreMultiTenancyApplicationBuilderExtensions
{
    public static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder app)
    {
        return app
            .UseMiddleware<MultiTenancyMiddleware>();
    }
}
