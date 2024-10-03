using Tiknas.TestApp.Testing;
using Xunit;

namespace Tiknas.MongoDB.Repositories;

[Collection(MongoTestCollection.Name)]
public class Repository_Queryable_Tests : Repository_Queryable_Tests<TiknasMongoDbTestModule>
{

}
