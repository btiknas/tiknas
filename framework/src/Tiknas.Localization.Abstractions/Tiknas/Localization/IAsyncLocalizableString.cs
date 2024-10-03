using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace Tiknas.Localization;

public interface IAsyncLocalizableString
{
    Task<LocalizedString> LocalizeAsync(IStringLocalizerFactory stringLocalizerFactory);
}