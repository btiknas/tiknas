using System.Threading.Tasks;
using Tiknas;
using Tiknas.Options;

namespace Microsoft.Extensions.Options;

public static class OptionsTiknasDynamicOptionsManagerExtensions
{
    public static Task SetAsync<T>(this IOptions<T> options)
        where T : class
    {
        return options.ToDynamicOptions().SetAsync();
    }

    public static Task SetAsync<T>(this IOptions<T> options, string name)
        where T : class
    {
        return options.ToDynamicOptions().SetAsync(name);
    }

    private static TiknasDynamicOptionsManager<T> ToDynamicOptions<T>(this IOptions<T> options)
        where T : class
    {
        if (options is TiknasDynamicOptionsManager<T> dynamicOptionsManager)
        {
            return dynamicOptionsManager;
        }

        throw new TiknasException($"Options must be derived from the {typeof(TiknasDynamicOptionsManager<>).FullName}!");
    }
}
