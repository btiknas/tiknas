using Tiknas.TestApp.Testing;
using Xunit;

namespace Tiknas.MongoDB.Uow;

[Collection(MongoTestCollection.Name)]
public class Uow_Completed_Tests : Uow_Completed_Tests<TiknasMongoDbTestModule>
{
}
