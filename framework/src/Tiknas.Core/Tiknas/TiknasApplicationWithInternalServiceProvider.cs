using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Tiknas;

internal class TiknasApplicationWithInternalServiceProvider : TiknasApplicationBase, ITiknasApplicationWithInternalServiceProvider
{
    public IServiceScope? ServiceScope { get; private set; }

    public TiknasApplicationWithInternalServiceProvider(
        [NotNull] Type startupModuleType,
        Action<TiknasApplicationCreationOptions>? optionsAction
        ) : this(
        startupModuleType,
        new ServiceCollection(),
        optionsAction)
    {

    }

    private TiknasApplicationWithInternalServiceProvider(
        [NotNull] Type startupModuleType,
        [NotNull] IServiceCollection services,
        Action<TiknasApplicationCreationOptions>? optionsAction
        ) : base(
            startupModuleType,
            services,
            optionsAction)
    {
        Services.AddSingleton<ITiknasApplicationWithInternalServiceProvider>(this);
    }

    public IServiceProvider CreateServiceProvider()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (ServiceProvider != null)
        {
            return ServiceProvider;
        }

        ServiceScope = Services.BuildServiceProviderFromFactory().CreateScope();
        SetServiceProvider(ServiceScope.ServiceProvider);

        return ServiceProvider!;
    }

    public async Task InitializeAsync()
    {
        CreateServiceProvider();
        await InitializeModulesAsync();
    }

    public void Initialize()
    {
        CreateServiceProvider();
        InitializeModules();
    }

    public override void Dispose()
    {
        base.Dispose();
        ServiceScope?.Dispose();
    }
}
