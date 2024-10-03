using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Mvc.AntiForgery;

public class TiknasAntiForgeryCookieNameProvider : ITransientDependency
{
    private readonly IOptionsMonitor<CookieAuthenticationOptions> _namedOptionsAccessor;
    private readonly TiknasAntiForgeryOptions _tiknasAntiForgeryOptions;

    public TiknasAntiForgeryCookieNameProvider(
        IOptionsMonitor<CookieAuthenticationOptions> namedOptionsAccessor,
        IOptions<TiknasAntiForgeryOptions> tiknasAntiForgeryOptions)
    {
        _namedOptionsAccessor = namedOptionsAccessor;
        _tiknasAntiForgeryOptions = tiknasAntiForgeryOptions.Value;
    }

    public virtual string? GetAuthCookieNameOrNull()
    {
        if (_tiknasAntiForgeryOptions.AuthCookieSchemaName == null)
        {
            return null;
        }

        return _namedOptionsAccessor.Get(_tiknasAntiForgeryOptions.AuthCookieSchemaName)?.Cookie?.Name;
    }

    public virtual string? GetAntiForgeryCookieNameOrNull()
    {
        return _tiknasAntiForgeryOptions.TokenCookie.Name;
    }
}
