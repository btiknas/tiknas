using Tiknas.TestApp.Testing;
using Xunit;

namespace Tiknas.MongoDB.DomainEvents;

[Collection(MongoTestCollection.Name)]
public class EntityChangeEvents_Tests : EntityChangeEvents_Tests<TiknasMongoDbTestModule>
{

}
