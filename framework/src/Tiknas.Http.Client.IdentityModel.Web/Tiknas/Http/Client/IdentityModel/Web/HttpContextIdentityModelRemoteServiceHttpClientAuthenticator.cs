using System.Threading.Tasks;
using IdentityModel.Client;
using Tiknas.DependencyInjection;
using Tiknas.Http.Client.Authentication;
using Tiknas.IdentityModel;

namespace Tiknas.Http.Client.IdentityModel.Web;

[Dependency(ReplaceServices = true)]
public class HttpContextIdentityModelRemoteServiceHttpClientAuthenticator : IdentityModelRemoteServiceHttpClientAuthenticator
{
    protected ITiknasAccessTokenProvider AccessTokenProvider { get; }

    public HttpContextIdentityModelRemoteServiceHttpClientAuthenticator(
        IIdentityModelAuthenticationService identityModelAuthenticationService,
        ITiknasAccessTokenProvider accessTokenProvider)
        : base(identityModelAuthenticationService)
    {
        AccessTokenProvider = accessTokenProvider;
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
