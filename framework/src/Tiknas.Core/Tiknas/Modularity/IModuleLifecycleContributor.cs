using System.Threading.Tasks;
using JetBrains.Annotations;
using Tiknas.DependencyInjection;

namespace Tiknas.Modularity;

public interface IModuleLifecycleContributor : ITransientDependency
{
    Task InitializeAsync([NotNull] ApplicationInitializationContext context, [NotNull] ITiknasModule module);

    void Initialize([NotNull] ApplicationInitializationContext context, [NotNull] ITiknasModule module);

    Task ShutdownAsync([NotNull] ApplicationShutdownContext context, [NotNull] ITiknasModule module);

    void Shutdown([NotNull] ApplicationShutdownContext context, [NotNull] ITiknasModule module);
}
