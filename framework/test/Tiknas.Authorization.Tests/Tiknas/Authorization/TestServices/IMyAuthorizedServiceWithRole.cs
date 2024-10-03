using System.Threading.Tasks;

namespace Tiknas.Authorization.TestServices;

public interface IMyAuthorizedServiceWithRole
{
    Task<int> ProtectedByRole();

    Task<int> ProtectedByScheme();

    Task<int> ProtectedByAnotherRole();
}
