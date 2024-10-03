using Microsoft.Extensions.Localization;

namespace Tiknas.Localization;

public interface ILocalizableString
{
    LocalizedString Localize(IStringLocalizerFactory stringLocalizerFactory);
}