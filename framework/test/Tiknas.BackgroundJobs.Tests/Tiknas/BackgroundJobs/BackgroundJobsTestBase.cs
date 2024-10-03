using Tiknas.TestBase;

namespace Tiknas.BackgroundJobs;

public abstract class BackgroundJobsTestBase : TiknasIntegratedTest<TiknasBackgroundJobsTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
