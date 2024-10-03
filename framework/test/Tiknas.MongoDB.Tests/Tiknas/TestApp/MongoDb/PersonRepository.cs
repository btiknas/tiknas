using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Tiknas.Domain.Repositories.MongoDB;
using Tiknas.MongoDB;
using Tiknas.TestApp.Domain;

namespace Tiknas.TestApp.MongoDB;

public class PersonRepository : MongoDbRepository<ITestAppMongoDbContext, Person, Guid>, IPersonRepository
{
    public PersonRepository(IMongoDbContextProvider<ITestAppMongoDbContext> dbContextProvider)
        : base(dbContextProvider)
    {

    }

    public async Task<PersonView> GetViewAsync(string name)
    {
        var person = await (await (await GetCollectionAsync()).FindAsync(x => x.Name == name)).FirstOrDefaultAsync();
        return new PersonView()
        {
            Name = person.Name,
            CreationTime = person.CreationTime,
            Birthday = person.Birthday,
            LastActive = person.LastActive
        };
    }
}
