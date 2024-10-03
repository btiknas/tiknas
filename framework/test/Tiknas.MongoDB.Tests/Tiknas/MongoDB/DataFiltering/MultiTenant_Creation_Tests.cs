using Tiknas.TestApp.Testing;
using Xunit;

namespace Tiknas.MongoDB.DataFiltering;

[Collection(MongoTestCollection.Name)]
public class MultiTenant_Creation_Tests : MultiTenant_Creation_Tests<TiknasMongoDbTestModule>
{

}
