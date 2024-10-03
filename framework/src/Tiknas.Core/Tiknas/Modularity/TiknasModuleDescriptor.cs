using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using JetBrains.Annotations;

namespace Tiknas.Modularity;

public class TiknasModuleDescriptor : ITiknasModuleDescriptor
{
    public Type Type { get; }

    public Assembly Assembly { get; }
    
    public Assembly[] AllAssemblies { get; }

    public ITiknasModule Instance { get; }

    public bool IsLoadedAsPlugIn { get; }

    public IReadOnlyList<ITiknasModuleDescriptor> Dependencies => _dependencies.ToImmutableList();
    private readonly List<ITiknasModuleDescriptor> _dependencies;

    public TiknasModuleDescriptor(
        [NotNull] Type type,
        [NotNull] ITiknasModule instance,
        bool isLoadedAsPlugIn)
    {
        Check.NotNull(type, nameof(type));
        Check.NotNull(instance, nameof(instance));
        TiknasModule.CheckTiknasModuleType(type);

        if (!type.GetTypeInfo().IsAssignableFrom(instance.GetType()))
        {
            throw new ArgumentException($"Given module instance ({instance.GetType().AssemblyQualifiedName}) is not an instance of given module type: {type.AssemblyQualifiedName}");
        }

        Type = type;
        Assembly = type.Assembly;
        AllAssemblies = TiknasModuleHelper.GetAllAssemblies(type);
        Instance = instance;
        IsLoadedAsPlugIn = isLoadedAsPlugIn;

        _dependencies = new List<ITiknasModuleDescriptor>();
    }

    public void AddDependency(ITiknasModuleDescriptor descriptor)
    {
        _dependencies.AddIfNotContains(descriptor);
    }

    public override string ToString()
    {
        return $"[TiknasModuleDescriptor {Type.FullName}]";
    }
}
