using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Tiknas.DependencyInjection;
using Tiknas.Http.Client.Authentication;

namespace Tiknas.Http.Client.IdentityModel.WebAssembly;

[Dependency(ReplaceServices = true)]
public class WebAssemblyTiknasAccessTokenProvider : ITiknasAccessTokenProvider, ITransientDependency
{
    protected IAccessTokenProvider? AccessTokenProvider { get; }

    public WebAssemblyTiknasAccessTokenProvider(IAccessTokenProvider accessTokenProvider)
    {
        AccessTokenProvider = accessTokenProvider;
    }

    public virtual async Task<string?> GetTokenAsync()
    {
        if (AccessTokenProvider == null)
        {
            return null;
        }

        var result = await AccessTokenProvider.RequestAccessToken();
        if (result.Status != AccessTokenResultStatus.Success)
        {
            return null;
        }

        result.TryGetToken(out var token);
        return token?.Value;
    }
}
