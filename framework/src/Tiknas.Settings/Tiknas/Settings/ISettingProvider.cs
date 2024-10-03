using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Tiknas.Settings;

public interface ISettingProvider
{
    Task<string?> GetOrNullAsync([NotNull] string name);

    Task<List<SettingValue>> GetAllAsync([NotNull] string[] names);

    Task<List<SettingValue>> GetAllAsync();
}
