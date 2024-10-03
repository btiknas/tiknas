using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tiknas.Domain.Repositories;
using Tiknas.Domain.Repositories.EntityFrameworkCore;
using Tiknas.EntityFrameworkCore;
using Tiknas.TestApp.Domain;

namespace Tiknas.TestApp.EntityFrameworkCore;

public class CityRepository : EfCoreRepository<TestAppDbContext, City, Guid>, ICityRepository
{
    public CityRepository(IDbContextProvider<TestAppDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<City> FindByNameAsync(string name)
    {
        return await this.FirstOrDefaultAsync(c => c.Name == name);
    }

    public async Task<List<Person>> GetPeopleInTheCityAsync(string cityName)
    {
        var city = await FindByNameAsync(cityName);
        return await (await GetDbContextAsync()).People.Where(p => p.CityId == city.Id).ToListAsync();
    }
}
