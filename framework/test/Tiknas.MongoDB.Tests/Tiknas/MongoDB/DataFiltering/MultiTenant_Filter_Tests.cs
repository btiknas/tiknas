using Tiknas.TestApp.Testing;
using Xunit;

namespace Tiknas.MongoDB.DataFiltering;

[Collection(MongoTestCollection.Name)]
public class MultiTenant_Filter_Tests : MultiTenant_Filter_Tests<TiknasMongoDbTestModule>
{

}
