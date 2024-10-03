namespace Tiknas.BackgroundWorkers.Quartz;

public interface IQuartzBackgroundWorkerAdapter : IQuartzBackgroundWorker
{
    void BuildWorker(IBackgroundWorker worker);
}
