using JetBrains.Annotations;

namespace Tiknas.Localization;

public interface IHasNameWithLocalizableDisplayName
{
    [NotNull]
    public string Name { get; }

    public ILocalizableString? DisplayName { get; }
}
