using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Tiknas.DependencyInjection;

namespace Tiknas.Settings;

public class SettingDefinitionManager : ISettingDefinitionManager, ISingletonDependency
{
    protected readonly IStaticSettingDefinitionStore StaticStore;
    protected readonly IDynamicSettingDefinitionStore DynamicStore;

    public SettingDefinitionManager(IStaticSettingDefinitionStore staticStore, IDynamicSettingDefinitionStore dynamicStore)
    {
        StaticStore = staticStore;
        DynamicStore = dynamicStore;
    }

    public virtual async Task<SettingDefinition> GetAsync(string name)
    {
        var setting = await GetOrNullAsync(name);
        if (setting == null)
        {
            throw new TiknasException("Undefined setting: " + name);
        }

        return setting;
    }

    public virtual async Task<SettingDefinition?> GetOrNullAsync(string name)
    {
        Check.NotNull(name, nameof(name));

        return await StaticStore.GetOrNullAsync(name) ?? await DynamicStore.GetOrNullAsync(name);
    }

    public virtual async Task<IReadOnlyList<SettingDefinition>> GetAllAsync()
    {
        var staticSettings = await StaticStore.GetAllAsync();
        var staticSettingNames = staticSettings
            .Select(p => p.Name)
            .ToImmutableHashSet();

        var dynamicSettings = await DynamicStore.GetAllAsync();

        /* We prefer static settings over dynamics */
        return staticSettings.Concat(dynamicSettings.Where(d => !staticSettingNames.Contains(d.Name))).ToImmutableList();
    }
}
