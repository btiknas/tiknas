using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiknas.Domain.Repositories.MemoryDb;
using Tiknas.MemoryDb;
using Tiknas.TestApp.Domain;

namespace Tiknas.TestApp.MemoryDb;

public class CityRepository : MemoryDbRepository<TestAppMemoryDbContext, City, Guid>, ICityRepository
{
    public CityRepository(IMemoryDatabaseProvider<TestAppMemoryDbContext> databaseProvider)
        : base(databaseProvider)
    {
    }

    public async Task<City> FindByNameAsync(string name)
    {
        return (await GetCollectionAsync()).FirstOrDefault(c => c.Name == name);
    }

    public async Task<List<Person>> GetPeopleInTheCityAsync(string cityName)
    {
        var city = await FindByNameAsync(cityName);

        return (await GetDatabaseAsync()).Collection<Person>().Where(p => p.CityId == city.Id).ToList();
    }
}
