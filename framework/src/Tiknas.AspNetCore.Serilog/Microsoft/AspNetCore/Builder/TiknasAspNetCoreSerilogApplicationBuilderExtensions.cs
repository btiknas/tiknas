using Tiknas.AspNetCore.Serilog;

namespace Microsoft.AspNetCore.Builder;

public static class TiknasAspNetCoreSerilogApplicationBuilderExtensions
{
    public static IApplicationBuilder UseTiknasSerilogEnrichers(this IApplicationBuilder app)
    {
        return app
            .UseMiddleware<TiknasSerilogMiddleware>();
    }
}
