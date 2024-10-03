using System.Threading.Tasks;

namespace Tiknas.Features;

public interface IMethodInvocationFeatureCheckerService
{
    Task CheckAsync(
        MethodInvocationFeatureCheckerContext context
    );
}
