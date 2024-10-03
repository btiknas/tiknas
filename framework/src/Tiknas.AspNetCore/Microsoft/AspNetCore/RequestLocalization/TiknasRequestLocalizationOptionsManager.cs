using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Tiknas.Options;

namespace Microsoft.AspNetCore.RequestLocalization;

public class TiknasRequestLocalizationOptionsManager : TiknasDynamicOptionsManager<RequestLocalizationOptions>
{
    private RequestLocalizationOptions? _options;

    private readonly ITiknasRequestLocalizationOptionsProvider _tiknasRequestLocalizationOptionsProvider;

    public TiknasRequestLocalizationOptionsManager(
        IOptionsFactory<RequestLocalizationOptions> factory,
        ITiknasRequestLocalizationOptionsProvider tiknasRequestLocalizationOptionsProvider)
        : base(factory)
    {
        _tiknasRequestLocalizationOptionsProvider = tiknasRequestLocalizationOptionsProvider;
    }

    public override RequestLocalizationOptions Get(string? name)
    {
        return _options ?? base.Get(name);
    }

    protected override async Task OverrideOptionsAsync(string name, RequestLocalizationOptions options)
    {
        _options = await _tiknasRequestLocalizationOptionsProvider.GetLocalizationOptionsAsync();
    }
}
