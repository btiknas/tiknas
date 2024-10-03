using Tiknas.Autofac;
using Tiknas.Modularity;

namespace Tiknas.BackgroundJobs;

[DependsOn(
    typeof(TiknasBackgroundJobsModule),
    typeof(TiknasAutofacModule),
    typeof(TiknasTestBaseModule)
)]
public class TiknasBackgroundJobsTestModule : TiknasModule
{

}
