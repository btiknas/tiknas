using System.Threading.Tasks;
using Tiknas.Http.Client.Authentication;

namespace Tiknas.AspNetCore.Components.WebAssembly.WebApp;

public class CookieBasedWebAssemblyTiknasAccessTokenProvider : ITiknasAccessTokenProvider
{
    public virtual Task<string?> GetTokenAsync()
    {
        return Task.FromResult<string?>(null);
    }
}
