using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.Modularity.PlugIns;

namespace Tiknas.Modularity;

public class ModuleLoader : IModuleLoader
{
    public ITiknasModuleDescriptor[] LoadModules(
        IServiceCollection services,
        Type startupModuleType,
        PlugInSourceList plugInSources)
    {
        Check.NotNull(services, nameof(services));
        Check.NotNull(startupModuleType, nameof(startupModuleType));
        Check.NotNull(plugInSources, nameof(plugInSources));

        var modules = GetDescriptors(services, startupModuleType, plugInSources);

        modules = SortByDependency(modules, startupModuleType);

        return modules.ToArray();
    }

    private List<ITiknasModuleDescriptor> GetDescriptors(
        IServiceCollection services,
        Type startupModuleType,
        PlugInSourceList plugInSources)
    {
        var modules = new List<TiknasModuleDescriptor>();

        FillModules(modules, services, startupModuleType, plugInSources);
        SetDependencies(modules);

        return modules.Cast<ITiknasModuleDescriptor>().ToList();
    }

    protected virtual void FillModules(
        List<TiknasModuleDescriptor> modules,
        IServiceCollection services,
        Type startupModuleType,
        PlugInSourceList plugInSources)
    {
        var logger = services.GetInitLogger<TiknasApplicationBase>();

        //All modules starting from the startup module
        foreach (var moduleType in TiknasModuleHelper.FindAllModuleTypes(startupModuleType, logger))
        {
            modules.Add(CreateModuleDescriptor(services, moduleType));
        }

        //Plugin modules
        foreach (var moduleType in plugInSources.GetAllModules(logger))
        {
            if (modules.Any(m => m.Type == moduleType))
            {
                continue;
            }

            modules.Add(CreateModuleDescriptor(services, moduleType, isLoadedAsPlugIn: true));
        }
    }

    protected virtual void SetDependencies(List<TiknasModuleDescriptor> modules)
    {
        foreach (var module in modules)
        {
            SetDependencies(modules, module);
        }
    }

    protected virtual List<ITiknasModuleDescriptor> SortByDependency(List<ITiknasModuleDescriptor> modules, Type startupModuleType)
    {
        var sortedModules = modules.SortByDependencies(m => m.Dependencies);
        sortedModules.MoveItem(m => m.Type == startupModuleType, modules.Count - 1);
        return sortedModules;
    }

    protected virtual TiknasModuleDescriptor CreateModuleDescriptor(IServiceCollection services, Type moduleType, bool isLoadedAsPlugIn = false)
    {
        return new TiknasModuleDescriptor(moduleType, CreateAndRegisterModule(services, moduleType), isLoadedAsPlugIn);
    }

    protected virtual ITiknasModule CreateAndRegisterModule(IServiceCollection services, Type moduleType)
    {
        var module = (ITiknasModule)Activator.CreateInstance(moduleType)!;
        services.AddSingleton(moduleType, module);
        return module;
    }

    protected virtual void SetDependencies(List<TiknasModuleDescriptor> modules, TiknasModuleDescriptor module)
    {
        foreach (var dependedModuleType in TiknasModuleHelper.FindDependedModuleTypes(module.Type))
        {
            var dependedModule = modules.FirstOrDefault(m => m.Type == dependedModuleType);
            if (dependedModule == null)
            {
                throw new TiknasException("Could not find a depended module " + dependedModuleType.AssemblyQualifiedName + " for " + module.Type.AssemblyQualifiedName);
            }

            module.AddDependency(dependedModule);
        }
    }
}
