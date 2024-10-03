using Tiknas.TestApp.Testing;
using Xunit;

namespace Tiknas.MongoDB.Domain;

[Collection(MongoTestCollection.Name)]
public class EntityChange_Tests : EntityChange_Tests<TiknasMongoDbTestModule>
{

}
