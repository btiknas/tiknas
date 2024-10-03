using System.Threading.Tasks;

namespace Tiknas.Http.Client.Authentication;

public interface ITiknasAccessTokenProvider
{
    Task<string?> GetTokenAsync();
}
