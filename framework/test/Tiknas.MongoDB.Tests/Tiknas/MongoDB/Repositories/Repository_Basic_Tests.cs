using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using Shouldly;
using Tiknas.Domain.Repositories;
using Tiknas.TestApp;
using Tiknas.TestApp.Domain;
using Tiknas.TestApp.Testing;
using Xunit;

namespace Tiknas.MongoDB.Repositories;

[Collection(MongoTestCollection.Name)]
public class Repository_Basic_Tests : Repository_Basic_Tests<TiknasMongoDbTestModule>
{
    [Fact]
    public async Task ToMongoQueryable_Test()
    {
        (await PersonRepository.GetQueryableAsync()).ShouldNotBeNull();
        (await PersonRepository.GetQueryableAsync()).As<IMongoQueryable<Person>>().ShouldNotBeNull();
        ((IMongoQueryable<Person>)(await PersonRepository.GetQueryableAsync()).Where(p => p.Name == "Douglas")).ShouldNotBeNull();
        (await PersonRepository.GetQueryableAsync()).Where(p => p.Name == "Douglas").As<IMongoQueryable<Person>>().ShouldNotBeNull();
    }

    [Fact]
    public async Task Linq_Queries()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            (await PersonRepository.GetQueryableAsync()).FirstOrDefault(p => p.Name == "Douglas").ShouldNotBeNull();
            (await PersonRepository.GetQueryableAsync()).Count().ShouldBeGreaterThan(0);
            return Task.CompletedTask;
        });
    }

    [Fact]
    public async Task UpdateAsync()
    {
        var person = await PersonRepository.GetAsync(TestDataBuilder.UserDouglasId);

        person.ChangeName("Douglas-Updated");
        person.Phones.Add(new Phone(person.Id, "6667778899", PhoneType.Office));

        await PersonRepository.UpdateAsync(person);

        person = await PersonRepository.FindAsync(TestDataBuilder.UserDouglasId);
        person.ShouldNotBeNull();
        person.Name.ShouldBe("Douglas-Updated");
        person.Phones.Count.ShouldBe(3);
        person.Phones.Any(p => p.PersonId == person.Id && p.Number == "6667778899" && p.Type == PhoneType.Office).ShouldBeTrue();
    }

    [Fact]
    public override async Task InsertAsync()
    {
        var person = new Person(Guid.NewGuid(), "New Person", 35);
        person.Phones.Add(new Phone(person.Id, "1234567890"));

        await PersonRepository.InsertAsync(person);

        person = await PersonRepository.FindAsync(person.Id);
        person.ShouldNotBeNull();
        person.Name.ShouldBe("New Person");
        person.Phones.Count.ShouldBe(1);
        person.Phones.Any(p => p.PersonId == person.Id && p.Number == "1234567890").ShouldBeTrue();
    }
    
    [Fact]
    public async Task Filter_Case_Insensitive()
    {
        (await CityRepository.GetMongoQueryableAsync()).FirstOrDefault(c => c.Name == "ISTANBUL").ShouldBeNull();
        (await CityRepository.GetMongoQueryableAsync()).FirstOrDefault(c => c.Name == "istanbul").ShouldBeNull();
        (await CityRepository.GetMongoQueryableAsync()).FirstOrDefault(c => c.Name == "Istanbul").ShouldNotBeNull();
        
        (await PersonRepository.GetQueryableAsync()).FirstOrDefault(p => p.Name == "douglas").ShouldNotBeNull();
        (await PersonRepository.GetQueryableAsync()).FirstOrDefault(p => p.Name == "DOUGLAS").ShouldNotBeNull();
        (await PersonRepository.GetQueryableAsync()).FirstOrDefault(p => p.Name == "Douglas").ShouldNotBeNull();
    }
}
