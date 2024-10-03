using System.Collections.Generic;
using System.Threading.Tasks;
using Tiknas.DependencyInjection;

namespace Tiknas.Settings;

public abstract class SettingValueProvider : ISettingValueProvider, ITransientDependency
{
    public abstract string Name { get; }

    protected ISettingStore SettingStore { get; }

    protected SettingValueProvider(ISettingStore settingStore)
    {
        SettingStore = settingStore;
    }

    public abstract Task<string?> GetOrNullAsync(SettingDefinition setting);

    public abstract Task<List<SettingValue>> GetAllAsync(SettingDefinition[] settings);
}
