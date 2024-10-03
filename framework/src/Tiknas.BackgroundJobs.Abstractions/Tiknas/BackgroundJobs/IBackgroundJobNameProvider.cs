namespace Tiknas.BackgroundJobs;

public interface IBackgroundJobNameProvider
{
    string Name { get; }
}
