using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.Modularity;

namespace Tiknas.TestBase;

public class TiknasAsyncIntegratedTest<TStartupModule> : TiknasTestBaseWithServiceProvider
    where TStartupModule : ITiknasModule
{
    protected ITiknasApplication Application { get; set; } = default!;

    protected IServiceProvider RootServiceProvider { get; set; } = default!;

    protected IServiceScope TestServiceScope { get; set; } = default!;

    public virtual async Task InitializeAsync()
    {
        var services = await CreateServiceCollectionAsync();

        await BeforeAddApplicationAsync(services);
        var application = await services.AddApplicationAsync<TStartupModule>(await SetTiknasApplicationCreationOptionsAsync());
        await AfterAddApplicationAsync(services);

        RootServiceProvider = await CreateServiceProviderAsync(services);
        TestServiceScope = RootServiceProvider.CreateScope();
        await application.InitializeAsync(TestServiceScope.ServiceProvider);
        ServiceProvider = application.ServiceProvider;
        Application = application;

        await InitializeServicesAsync();
    }

    public virtual async Task DisposeAsync()
    {
        await Application.ShutdownAsync();
        if (RootServiceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }
        TestServiceScope.Dispose();
        Application.Dispose();
    }

    protected virtual Task<IServiceCollection> CreateServiceCollectionAsync()
    {
        return Task.FromResult<IServiceCollection>(new ServiceCollection());
    }

    protected virtual Task BeforeAddApplicationAsync(IServiceCollection services)
    {
        return Task.CompletedTask;
    }

    protected virtual Task<Action<TiknasApplicationCreationOptions>> SetTiknasApplicationCreationOptionsAsync()
    {
        return Task.FromResult<Action<TiknasApplicationCreationOptions>>(_ => { });
    }

    protected virtual Task AfterAddApplicationAsync(IServiceCollection services)
    {
        return Task.CompletedTask;
    }

    protected virtual Task<IServiceProvider> CreateServiceProviderAsync(IServiceCollection services)
    {
        return Task.FromResult(services.BuildServiceProviderFromFactory());
    }

    protected virtual Task InitializeServicesAsync()
    {
        return Task.CompletedTask;
    }
}
