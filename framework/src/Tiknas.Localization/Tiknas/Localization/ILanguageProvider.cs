using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tiknas.Localization;

public interface ILanguageProvider
{
    Task<IReadOnlyList<LanguageInfo>> GetLanguagesAsync();
}
