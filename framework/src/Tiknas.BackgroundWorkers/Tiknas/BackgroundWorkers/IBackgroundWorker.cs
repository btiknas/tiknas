using Tiknas.DependencyInjection;
using Tiknas.Threading;

namespace Tiknas.BackgroundWorkers;

/// <summary>
/// Interface for a worker (thread) that runs on background to perform some tasks.
/// </summary>
public interface IBackgroundWorker : IRunnable, ISingletonDependency
{

}
