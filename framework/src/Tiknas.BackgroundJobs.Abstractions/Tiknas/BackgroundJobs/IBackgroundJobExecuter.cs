using System.Threading.Tasks;

namespace Tiknas.BackgroundJobs;

public interface IBackgroundJobExecuter
{
    Task ExecuteAsync(JobExecutionContext context);
}
