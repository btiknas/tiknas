using Tiknas.TestApp.Testing;
using Xunit;

namespace Tiknas.MongoDB.DataFiltering;

[Collection(MongoTestCollection.Name)]
public class SoftDelete_Tests : SoftDelete_Tests<TiknasMongoDbTestModule>
{

}
