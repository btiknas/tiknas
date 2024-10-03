using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Tiknas;

public interface IOnApplicationShutdown
{
    Task OnApplicationShutdownAsync([NotNull] ApplicationShutdownContext context);

    void OnApplicationShutdown([NotNull] ApplicationShutdownContext context);
}
