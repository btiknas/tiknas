using Tiknas.TestApp.Testing;
using Xunit;

namespace Tiknas.MongoDB.DomainEvents;

[Collection(MongoTestCollection.Name)]
public class DomainEvents_Tests : DomainEvents_Tests<TiknasMongoDbTestModule>
{
}
