using System.Threading.Tasks;
using IdentityModel.Client;
using Tiknas.DependencyInjection;
using Tiknas.Http.Client.Authentication;
using Tiknas.IdentityModel;

namespace Tiknas.Http.Client.IdentityModel.MauiBlazor;

[Dependency(ReplaceServices = true)]
public class MauiBlazorIdentityModelRemoteServiceHttpClientAuthenticator : IdentityModelRemoteServiceHttpClientAuthenticator
{
    protected ITiknasAccessTokenProvider AccessTokenProvider { get; }

    public MauiBlazorIdentityModelRemoteServiceHttpClientAuthenticator(
        IIdentityModelAuthenticationService identityModelAuthenticationService,
        ITiknasAccessTokenProvider tiknasAccessTokenProvider)
        : base(identityModelAuthenticationService)
    {
        AccessTokenProvider = tiknasAccessTokenProvider;
    }

    public async override Task Authenticate(RemoteServiceHttpClientAuthenticateContext context)
    {
        if (context.RemoteService.GetUseCurrentAccessToken() != false)
        {
            var accessToken = await AccessTokenProvider.GetTokenAsync();
            if (accessToken != null)
            {
                context.Request.SetBearerToken(accessToken);
                return;
            }
        }

        await base.Authenticate(context);
    }
}
