using Tiknas.TestApp.Testing;
using Xunit;

namespace Tiknas.MongoDB.Auditing;

[Collection(MongoTestCollection.Name)]
public class Auditing_Tests : Auditing_Tests<TiknasMongoDbTestModule>
{

}
