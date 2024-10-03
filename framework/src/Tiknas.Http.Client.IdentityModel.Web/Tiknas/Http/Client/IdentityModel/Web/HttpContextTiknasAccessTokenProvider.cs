using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Tiknas.DependencyInjection;
using Tiknas.Http.Client.Authentication;

namespace Tiknas.Http.Client.IdentityModel.Web;

[Dependency(ReplaceServices = true)]
public class HttpContextTiknasAccessTokenProvider : ITiknasAccessTokenProvider, ITransientDependency
{
    protected IHttpContextAccessor HttpContextAccessor { get; }

    public HttpContextTiknasAccessTokenProvider(IHttpContextAccessor httpContextAccessor)
    {
        HttpContextAccessor = httpContextAccessor;
    }

    public virtual async Task<string?> GetTokenAsync()
    {
        var httpContext = HttpContextAccessor?.HttpContext;
        if (httpContext == null)
        {
            return null;
        }

        return await httpContext.GetTokenAsync("access_token");
    }
}
