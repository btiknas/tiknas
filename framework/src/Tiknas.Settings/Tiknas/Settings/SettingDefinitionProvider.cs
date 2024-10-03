using Tiknas.DependencyInjection;

namespace Tiknas.Settings;

public abstract class SettingDefinitionProvider : ISettingDefinitionProvider, ITransientDependency
{
    public abstract void Define(ISettingDefinitionContext context);
}
