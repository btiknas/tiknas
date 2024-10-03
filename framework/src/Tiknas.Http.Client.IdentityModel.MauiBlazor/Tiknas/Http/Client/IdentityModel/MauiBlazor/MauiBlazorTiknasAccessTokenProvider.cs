using System.Threading.Tasks;
using Tiknas.DependencyInjection;
using Tiknas.Http.Client.Authentication;

namespace Tiknas.Http.Client.IdentityModel.MauiBlazor;

[Dependency(ReplaceServices = true)]
public class MauiBlazorTiknasAccessTokenProvider : ITiknasAccessTokenProvider, ITransientDependency
{
    public virtual Task<string?> GetTokenAsync()
    {
        return Task.FromResult(null as string);
    }
}
