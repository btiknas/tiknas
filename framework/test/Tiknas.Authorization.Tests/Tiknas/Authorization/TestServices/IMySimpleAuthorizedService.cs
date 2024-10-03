using System.Threading.Tasks;

namespace Tiknas.Authorization.TestServices;

public interface IMySimpleAuthorizedService
{
    Task<int> ProtectedByClassAsync();

    Task<int> AnonymousAsync();
}
