using MongoDB.Driver;

namespace Tiknas.MongoDB.TestApp.SecondContext;

public class SecondDbContext : TiknasMongoDbContext
{
    public IMongoCollection<BookInSecondDbContext> Books => Collection<BookInSecondDbContext>();

    public IMongoCollection<PhoneInSecondDbContext> Phones => Collection<PhoneInSecondDbContext>();
}
