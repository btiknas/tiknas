using System;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.Modularity;

namespace Tiknas.TestBase;

public abstract class TiknasIntegratedTest<TStartupModule> : TiknasTestBaseWithServiceProvider, IDisposable
    where TStartupModule : ITiknasModule
{
    protected ITiknasApplication Application { get; }

    protected IServiceProvider RootServiceProvider { get; }

    protected IServiceScope TestServiceScope { get; }

    protected TiknasIntegratedTest()
    {
        var services = CreateServiceCollection();

        BeforeAddApplication(services);

        var application = services.AddApplication<TStartupModule>(SetTiknasApplicationCreationOptions);
        Application = application;

        AfterAddApplication(services);

        RootServiceProvider = CreateServiceProvider(services);
        TestServiceScope = RootServiceProvider.CreateScope();

        application.Initialize(TestServiceScope.ServiceProvider);
        ServiceProvider = Application.ServiceProvider;
    }

    protected virtual IServiceCollection CreateServiceCollection()
    {
        return new ServiceCollection();
    }

    protected virtual void BeforeAddApplication(IServiceCollection services)
    {

    }

    protected virtual void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {

    }

    protected virtual void AfterAddApplication(IServiceCollection services)
    {

    }

    protected virtual IServiceProvider CreateServiceProvider(IServiceCollection services)
    {
        return services.BuildServiceProviderFromFactory();
    }

    public virtual void Dispose()
    {
        Application.Shutdown();
        TestServiceScope.Dispose();
        if (RootServiceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }
        Application.Dispose();
    }
}
