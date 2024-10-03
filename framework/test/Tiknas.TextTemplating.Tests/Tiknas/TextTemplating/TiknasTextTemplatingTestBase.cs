using Tiknas.Modularity;
using Tiknas.TestBase;

namespace Tiknas.TextTemplating;

public abstract class TiknasTextTemplatingTestBase<TStartupModule> : TiknasIntegratedTest<TStartupModule>
    where TStartupModule : ITiknasModule
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
