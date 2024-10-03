using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Tiknas.AspNetCore.TestBase;

public static class TiknasWebHostBuilderExtensions
{
    public static IWebHostBuilder UseTiknasTestServer(this IWebHostBuilder builder)
    {
        return builder.ConfigureServices(services =>
        {
            services.AddScoped<IHostLifetime, TiknasNoopHostLifetime>();
            services.AddScoped<IServer, TestServer>();
        });
    }
}
