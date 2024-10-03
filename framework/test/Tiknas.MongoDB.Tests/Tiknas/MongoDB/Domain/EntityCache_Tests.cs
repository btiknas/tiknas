using Tiknas.TestApp.Testing;
using Xunit;

namespace Tiknas.MongoDB.Domain;

[Collection(MongoTestCollection.Name)]
public class EntityCache_Tests : EntityCache_Tests<TiknasMongoDbTestModule>
{
}