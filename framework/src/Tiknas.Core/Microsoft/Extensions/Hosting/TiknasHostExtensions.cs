using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Tiknas;
using Tiknas.Threading;

namespace Microsoft.Extensions.Hosting;

public static class TiknasHostExtensions
{
    public static async Task InitializeAsync(this IHost host)
    {
        var application = host.Services.GetRequiredService<ITiknasApplicationWithExternalServiceProvider>();
        var applicationLifetime = host.Services.GetRequiredService<IHostApplicationLifetime>();

        applicationLifetime.ApplicationStopping.Register(() => AsyncHelper.RunSync(() => application.ShutdownAsync()));
        applicationLifetime.ApplicationStopped.Register(() => application.Dispose());

        await application.InitializeAsync(host.Services);
    }
}
