using Tiknas.TestApp.Testing;
using Xunit;

namespace Tiknas.MongoDB.DataFiltering;

[Collection(MongoTestCollection.Name)]
public class SoftDelete_Filter_Tests : SoftDelete_Filter_Tests<TiknasMongoDbTestModule>
{

}
