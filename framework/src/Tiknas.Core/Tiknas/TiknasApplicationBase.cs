using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Tiknas.DependencyInjection;
using Tiknas.Internal;
using Tiknas.Logging;
using Tiknas.Modularity;

namespace Tiknas;

public abstract class TiknasApplicationBase : ITiknasApplication
{
    [NotNull]
    public Type StartupModuleType { get; }

    public IServiceProvider ServiceProvider { get; private set; } = default!;

    public IServiceCollection Services { get; }

    public IReadOnlyList<ITiknasModuleDescriptor> Modules { get; }

    public string? ApplicationName { get; }

    public string InstanceId { get; } = Guid.NewGuid().ToString();

    private bool _configuredServices;

    internal TiknasApplicationBase(
        [NotNull] Type startupModuleType,
        [NotNull] IServiceCollection services,
        Action<TiknasApplicationCreationOptions>? optionsAction)
    {
        Check.NotNull(startupModuleType, nameof(startupModuleType));
        Check.NotNull(services, nameof(services));

        StartupModuleType = startupModuleType;
        Services = services;

        services.TryAddObjectAccessor<IServiceProvider>();

        var options = new TiknasApplicationCreationOptions(services);
        optionsAction?.Invoke(options);

        ApplicationName = GetApplicationName(options);

        services.AddSingleton<ITiknasApplication>(this);
        services.AddSingleton<IApplicationInfoAccessor>(this);
        services.AddSingleton<IModuleContainer>(this);
        services.AddSingleton<ITiknasHostEnvironment>(new TiknasHostEnvironment()
        {
            EnvironmentName = options.Environment
        });

        services.AddCoreServices();
        services.AddCoreTiknasServices(this, options);

        Modules = LoadModules(services, options);

        if (!options.SkipConfigureServices)
        {
            ConfigureServices();
        }
    }

    public virtual async Task ShutdownAsync()
    {
        using (var scope = ServiceProvider.CreateScope())
        {
            await scope.ServiceProvider
                .GetRequiredService<IModuleManager>()
                .ShutdownModulesAsync(new ApplicationShutdownContext(scope.ServiceProvider));
        }
    }

    public virtual void Shutdown()
    {
        using (var scope = ServiceProvider.CreateScope())
        {
            scope.ServiceProvider
                .GetRequiredService<IModuleManager>()
                .ShutdownModules(new ApplicationShutdownContext(scope.ServiceProvider));
        }
    }

    public virtual void Dispose()
    {
        //TODO: Shutdown if not done before?
    }

    protected virtual void SetServiceProvider(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        ServiceProvider.GetRequiredService<ObjectAccessor<IServiceProvider>>().Value = ServiceProvider;
    }

    protected virtual async Task InitializeModulesAsync()
    {
        using (var scope = ServiceProvider.CreateScope())
        {
            WriteInitLogs(scope.ServiceProvider);
            await scope.ServiceProvider
                .GetRequiredService<IModuleManager>()
                .InitializeModulesAsync(new ApplicationInitializationContext(scope.ServiceProvider));
        }
    }

    protected virtual void InitializeModules()
    {
        using (var scope = ServiceProvider.CreateScope())
        {
            WriteInitLogs(scope.ServiceProvider);
            scope.ServiceProvider
                .GetRequiredService<IModuleManager>()
                .InitializeModules(new ApplicationInitializationContext(scope.ServiceProvider));
        }
    }

    protected virtual void WriteInitLogs(IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetService<ILogger<TiknasApplicationBase>>();
        if (logger == null)
        {
            return;
        }

        var initLogger = serviceProvider.GetRequiredService<IInitLoggerFactory>().Create<TiknasApplicationBase>();

        foreach (var entry in initLogger.Entries)
        {
            logger.Log(entry.LogLevel, entry.EventId, entry.State, entry.Exception, entry.Formatter);
        }

        initLogger.Entries.Clear();
    }

    protected virtual IReadOnlyList<ITiknasModuleDescriptor> LoadModules(IServiceCollection services, TiknasApplicationCreationOptions options)
    {
        return services
            .GetSingletonInstance<IModuleLoader>()
            .LoadModules(
                services,
                StartupModuleType,
                options.PlugInSources
            );
    }

    //TODO: We can extract a new class for this
    public virtual async Task ConfigureServicesAsync()
    {
        CheckMultipleConfigureServices();

        var context = new ServiceConfigurationContext(Services);
        Services.AddSingleton(context);

        foreach (var module in Modules)
        {
            if (module.Instance is TiknasModule tiknasModule)
            {
                tiknasModule.ServiceConfigurationContext = context;
            }
        }

        //PreConfigureServices
        foreach (var module in Modules.Where(m => m.Instance is IPreConfigureServices))
        {
            try
            {
                await ((IPreConfigureServices)module.Instance).PreConfigureServicesAsync(context);
            }
            catch (Exception ex)
            {
                throw new TiknasInitializationException($"An error occurred during {nameof(IPreConfigureServices.PreConfigureServicesAsync)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.", ex);
            }
        }

        var assemblies = new HashSet<Assembly>();

        //ConfigureServices
        foreach (var module in Modules)
        {
            if (module.Instance is TiknasModule tiknasModule)
            {
                if (!tiknasModule.SkipAutoServiceRegistration)
                {
                    foreach (var assembly in module.AllAssemblies)
                    {
                        if (!assemblies.Contains(assembly))
                        {
                            Services.AddAssembly(assembly);
                            assemblies.Add(assembly);
                        }
                    }
                }
            }

            try
            {
                await module.Instance.ConfigureServicesAsync(context);
            }
            catch (Exception ex)
            {
                throw new TiknasInitializationException($"An error occurred during {nameof(ITiknasModule.ConfigureServicesAsync)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.", ex);
            }
        }

        //PostConfigureServices
        foreach (var module in Modules.Where(m => m.Instance is IPostConfigureServices))
        {
            try
            {
                await ((IPostConfigureServices)module.Instance).PostConfigureServicesAsync(context);
            }
            catch (Exception ex)
            {
                throw new TiknasInitializationException($"An error occurred during {nameof(IPostConfigureServices.PostConfigureServicesAsync)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.", ex);
            }
        }

        foreach (var module in Modules)
        {
            if (module.Instance is TiknasModule tiknasModule)
            {
                tiknasModule.ServiceConfigurationContext = null!;
            }
        }

        _configuredServices = true;

        TryToSetEnvironment(Services);
    }

    private void CheckMultipleConfigureServices()
    {
        if (_configuredServices)
        {
            throw new TiknasInitializationException("Services have already been configured! If you call ConfigureServicesAsync method, you must have set TiknasApplicationCreationOptions.SkipConfigureServices to true before.");
        }
    }

    //TODO: We can extract a new class for this
    public virtual void ConfigureServices()
    {
        CheckMultipleConfigureServices();

        var context = new ServiceConfigurationContext(Services);
        Services.AddSingleton(context);

        foreach (var module in Modules)
        {
            if (module.Instance is TiknasModule tiknasModule)
            {
                tiknasModule.ServiceConfigurationContext = context;
            }
        }

        //PreConfigureServices
        foreach (var module in Modules.Where(m => m.Instance is IPreConfigureServices))
        {
            try
            {
                ((IPreConfigureServices)module.Instance).PreConfigureServices(context);
            }
            catch (Exception ex)
            {
                throw new TiknasInitializationException($"An error occurred during {nameof(IPreConfigureServices.PreConfigureServices)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.", ex);
            }
        }

        var assemblies = new HashSet<Assembly>();

        //ConfigureServices
        foreach (var module in Modules)
        {
            if (module.Instance is TiknasModule tiknasModule)
            {
                if (!tiknasModule.SkipAutoServiceRegistration)
                {
                    foreach (var assembly in module.AllAssemblies)
                    {
                        if (!assemblies.Contains(assembly))
                        {
                            Services.AddAssembly(assembly);
                            assemblies.Add(assembly);
                        }
                    }
                }
            }

            try
            {
                module.Instance.ConfigureServices(context);
            }
            catch (Exception ex)
            {
                throw new TiknasInitializationException($"An error occurred during {nameof(ITiknasModule.ConfigureServices)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.", ex);
            }
        }

        //PostConfigureServices
        foreach (var module in Modules.Where(m => m.Instance is IPostConfigureServices))
        {
            try
            {
                ((IPostConfigureServices)module.Instance).PostConfigureServices(context);
            }
            catch (Exception ex)
            {
                throw new TiknasInitializationException($"An error occurred during {nameof(IPostConfigureServices.PostConfigureServices)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.", ex);
            }
        }

        foreach (var module in Modules)
        {
            if (module.Instance is TiknasModule tiknasModule)
            {
                tiknasModule.ServiceConfigurationContext = null!;
            }
        }

        _configuredServices = true;

        TryToSetEnvironment(Services);
    }

    private static string? GetApplicationName(TiknasApplicationCreationOptions options)
    {
        if (!string.IsNullOrWhiteSpace(options.ApplicationName))
        {
            return options.ApplicationName!;
        }

        var configuration = options.Services.GetConfigurationOrNull();
        if (configuration != null)
        {
            var appNameConfig = configuration["ApplicationName"];
            if (!string.IsNullOrWhiteSpace(appNameConfig))
            {
                return appNameConfig!;
            }
        }

        var entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly != null)
        {
            return entryAssembly.GetName().Name;
        }

        return null;
    }

    private static void TryToSetEnvironment(IServiceCollection services)
    {
        var tiknasHostEnvironment = services.GetSingletonInstance<ITiknasHostEnvironment>();
        if (tiknasHostEnvironment.EnvironmentName.IsNullOrWhiteSpace())
        {
            tiknasHostEnvironment.EnvironmentName = Environments.Production;
        }
    }
}
