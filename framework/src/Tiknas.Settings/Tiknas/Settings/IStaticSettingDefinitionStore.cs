using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Tiknas.Settings;

public interface IStaticSettingDefinitionStore
{
    Task<SettingDefinition> GetAsync([NotNull] string name);

    Task<IReadOnlyList<SettingDefinition>> GetAllAsync();

    Task<SettingDefinition?> GetOrNullAsync([NotNull] string name);
}
