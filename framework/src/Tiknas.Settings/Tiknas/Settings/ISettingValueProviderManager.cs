using System.Collections.Generic;

namespace Tiknas.Settings;

public interface ISettingValueProviderManager
{
    List<ISettingValueProvider> Providers { get; }
}
