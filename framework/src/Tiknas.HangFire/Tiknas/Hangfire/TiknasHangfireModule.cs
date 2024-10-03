using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tiknas.Authorization;
using Tiknas.Modularity;

namespace Tiknas.Hangfire;

[DependsOn(typeof(TiknasAuthorizationAbstractionsModule))]
public class TiknasHangfireModule : TiknasModule
{
    private TiknasHangfireBackgroundJobServer? _backgroundJobServer;

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var preActions = context.Services.GetPreConfigureActions<IGlobalConfiguration>();
        context.Services.AddHangfire(configuration =>
        {
            preActions.Configure(configuration);
        });

        context.Services.AddSingleton(serviceProvider =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<TiknasHangfireOptions>>().Value;
            return new TiknasHangfireBackgroundJobServer(options.BackgroundJobServerFactory.Invoke(serviceProvider));
        });
    }
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        _backgroundJobServer = context.ServiceProvider.GetRequiredService<TiknasHangfireBackgroundJobServer>();
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        if (_backgroundJobServer == null)
        {
            return;
        }

        _backgroundJobServer.HangfireJobServer?.SendStop();
        _backgroundJobServer.HangfireJobServer?.Dispose();
    }
}
