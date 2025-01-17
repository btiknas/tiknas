﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Tiknas.Domain.Repositories.MongoDB;
using Tiknas.MongoDB;
using Tiknas.TestApp.Domain;

namespace Tiknas.TestApp.MongoDB;

public class CityRepository : MongoDbRepository<ITestAppMongoDbContext, City, Guid>, ICityRepository
{
    public CityRepository(IMongoDbContextProvider<ITestAppMongoDbContext> dbContextProvider)
        : base(dbContextProvider)
    {

    }

    public async Task<City> FindByNameAsync(string name)
    {
        return await (await (await GetCollectionAsync()).FindAsync(c => c.Name == name)).FirstOrDefaultAsync();
    }

    public async Task<List<Person>> GetPeopleInTheCityAsync(string cityName)
    {
        var city = await FindByNameAsync(cityName);
        return await (await GetDbContextAsync()).People.AsQueryable().Where(p => p.CityId == city.Id).ToListAsync();
    }
}
