using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Tiknas.MongoDB.TestApp.FourthContext;
using Tiknas.MongoDB.TestApp.SecondContext;
using Tiknas.MongoDB.TestApp.ThirdDbContext;
using Tiknas.TestApp.MongoDB;
using Xunit;

namespace Tiknas.MongoDB;

[Collection(MongoTestCollection.Name)]
public class TiknasMongoDbConventionalRegistrar_Tests : MongoDbTestBase
{
    [Fact]
    public void All_TiknasMongoDbContext_Should_Exposed_ITiknasMongoDbContext_Service()
    {
        var tiknasMongoDbContext = ServiceProvider.GetServices<ITiknasMongoDbContext>();
        tiknasMongoDbContext.ShouldContain(x => x is TestAppMongoDbContext);
        tiknasMongoDbContext.ShouldContain(x => x is SecondDbContext);
        tiknasMongoDbContext.ShouldContain(x => x is ThirdDbContext);
        tiknasMongoDbContext.ShouldContain(x => x is FourthDbContext);
    }
}
