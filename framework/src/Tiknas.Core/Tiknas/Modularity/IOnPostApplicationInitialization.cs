using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Tiknas.Modularity;

public interface IOnPostApplicationInitialization
{
    Task OnPostApplicationInitializationAsync([NotNull] ApplicationInitializationContext context);

    void OnPostApplicationInitialization([NotNull] ApplicationInitializationContext context);
}
