using System.Threading.Tasks;

namespace Tiknas.Modularity;

public abstract class ModuleLifecycleContributorBase : IModuleLifecycleContributor
{
    public virtual Task InitializeAsync(ApplicationInitializationContext context, ITiknasModule module)
    {
        return Task.CompletedTask;
    }

    public virtual void Initialize(ApplicationInitializationContext context, ITiknasModule module)
    {
    }

    public virtual Task ShutdownAsync(ApplicationShutdownContext context, ITiknasModule module)
    {
        return Task.CompletedTask;
    }

    public virtual void Shutdown(ApplicationShutdownContext context, ITiknasModule module)
    {
    }
}
