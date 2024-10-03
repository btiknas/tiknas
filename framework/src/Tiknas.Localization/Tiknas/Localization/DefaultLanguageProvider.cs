using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Tiknas.DependencyInjection;

namespace Tiknas.Localization;

public class DefaultLanguageProvider : ILanguageProvider, ITransientDependency
{
    protected TiknasLocalizationOptions Options { get; }

    public DefaultLanguageProvider(IOptions<TiknasLocalizationOptions> options)
    {
        Options = options.Value;
    }

    public Task<IReadOnlyList<LanguageInfo>> GetLanguagesAsync()
    {
        return Task.FromResult((IReadOnlyList<LanguageInfo>)Options.Languages);
    }
}
