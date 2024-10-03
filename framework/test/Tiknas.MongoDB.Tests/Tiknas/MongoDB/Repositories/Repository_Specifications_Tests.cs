using Tiknas.TestApp.Testing;
using Xunit;

namespace Tiknas.MongoDB.Repositories;

[Collection(MongoTestCollection.Name)]
public class Repository_Specifications_Tests : Repository_Specifications_Tests<TiknasMongoDbTestModule>
{
}
