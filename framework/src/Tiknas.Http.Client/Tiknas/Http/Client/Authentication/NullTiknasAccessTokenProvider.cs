using System.Threading.Tasks;
using Tiknas.DependencyInjection;

namespace Tiknas.Http.Client.Authentication;

public class NullTiknasAccessTokenProvider : ITiknasAccessTokenProvider, ITransientDependency
{
    public Task<string?> GetTokenAsync()
    {
        return Task.FromResult(null as string);
    }
}
