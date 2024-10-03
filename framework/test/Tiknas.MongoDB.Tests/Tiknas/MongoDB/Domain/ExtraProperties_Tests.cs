using Tiknas.TestApp.Testing;
using Xunit;

namespace Tiknas.MongoDB.Domain;

[Collection(MongoTestCollection.Name)]
public class ExtraProperties_Tests : ExtraProperties_Tests<TiknasMongoDbTestModule>
{

}
