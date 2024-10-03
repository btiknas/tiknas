using System.Threading.Tasks;
using Tiknas.Threading;

namespace Tiknas.BackgroundJobs.RabbitMQ;

public interface IJobQueueManager : IRunnable
{
    Task<IJobQueue<TArgs>> GetAsync<TArgs>();
}
