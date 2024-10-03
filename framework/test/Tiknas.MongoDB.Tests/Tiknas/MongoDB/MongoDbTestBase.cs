using Tiknas.TestBase;

namespace Tiknas.MongoDB;

public abstract class MongoDbTestBase : TiknasIntegratedTest<TiknasMongoDbTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
