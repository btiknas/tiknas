using Tiknas.TestApp.Testing;
using Xunit;

namespace Tiknas.MongoDB.Repositories;

[Collection(MongoTestCollection.Name)]
public class RepositoryExtensions_Tests : RepositoryExtensions_Tests<TiknasMongoDbTestModule>
{

}
