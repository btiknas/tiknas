using System.Threading.Tasks;

namespace Tiknas.Authorization;

public interface IMethodInvocationAuthorizationService
{
    Task CheckAsync(MethodInvocationAuthorizationContext context);
}
