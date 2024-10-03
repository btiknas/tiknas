using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Tiknas.Http.Client.Authentication;

namespace Tiknas.AspNetCore.Components.WebAssembly.WebApp;

public class PersistentComponentStateTiknasAccessTokenProvider : ITiknasAccessTokenProvider
{
    private string? AccessToken { get; set; }

    private readonly PersistentComponentState _persistentComponentState;

    public PersistentComponentStateTiknasAccessTokenProvider(PersistentComponentState persistentComponentState)
    {
        _persistentComponentState = persistentComponentState;
        AccessToken = null;
    }

    public virtual Task<string?> GetTokenAsync()
    {
        if (AccessToken != null)
        {
            return Task.FromResult<string?>(AccessToken);
        }

        AccessToken = _persistentComponentState.TryTakeFromJson<PersistentAccessToken>(PersistentAccessToken.Key, out var token)
            ? token?.AccessToken
            : null;

        return Task.FromResult(AccessToken);
    }
}
