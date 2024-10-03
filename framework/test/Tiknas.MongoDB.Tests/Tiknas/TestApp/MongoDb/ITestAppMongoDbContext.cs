using MongoDB.Driver;
using Tiknas.Data;
using Tiknas.MongoDB;
using Tiknas.TestApp.Domain;
using Tiknas.TestApp.Testing;

namespace Tiknas.TestApp.MongoDB;

[ConnectionStringName("TestApp")]
public interface ITestAppMongoDbContext : ITiknasMongoDbContext
{
    IMongoCollection<Person> People { get; }

    IMongoCollection<City> Cities { get; }

    IMongoCollection<Product> Products { get; }

    IMongoCollection<AppEntityWithNavigations> AppEntityWithNavigations { get; }
}
