using System;
using System.Threading.Tasks;
using Tiknas.DependencyInjection;
using Tiknas.Domain.Repositories;
using Tiknas.TestApp.Domain;
using Tiknas.TestApp.Testing;

namespace Tiknas.TestApp;

public class TestDataBuilder : ITransientDependency
{
    public static Guid TenantId1 { get; } = new Guid("55687dce-595c-41b4-a024-2a5e991ac8f4");
    public static Guid TenantId2 { get; } = new Guid("f522d19f-5a86-4278-98fb-0577319c544a");
    public static Guid UserDouglasId { get; } = new Guid("1fcf46b2-28c3-48d0-8bac-fa53268a2775");
    public static Guid UserJohnDeletedId { get; } = new Guid("1e28ca9f-df84-4f39-83fe-f5450ecbf5d4");

    public static Guid IstanbulCityId { get; } = new Guid("4d734a0e-3e6b-4bad-bb43-ef8cf1b09633");
    public static Guid LondonCityId { get; } = new Guid("27237527-605e-4652-a2a5-68e0e512da36");
    
    public static Guid ProductId { get; } = new Guid("c5b3fbd4-7b0b-4a72-8d99-ccf6c8c98f61");

    private readonly IBasicRepository<Person, Guid> _personRepository;
    private readonly ICityRepository _cityRepository;
    private readonly IRepository<EntityWithIntPk, int> _entityWithIntPksRepository;
    private readonly IRepository<Product, Guid> _productRepository;

    public TestDataBuilder(
        IBasicRepository<Person, Guid> personRepository,
        ICityRepository cityRepository,
        IRepository<EntityWithIntPk, int> entityWithIntPksRepository,
        IRepository<Product, Guid> productRepository)
    {
        _personRepository = personRepository;
        _cityRepository = cityRepository;
        _entityWithIntPksRepository = entityWithIntPksRepository;
        _productRepository = productRepository;
    }

    public async Task BuildAsync()
    {
        await AddCities();
        await AddPeople();
        await AddEntitiesWithPks();
        await AddProducts();
    }

    private async Task AddCities()
    {
        var istanbul = new City(IstanbulCityId, "Istanbul");
        istanbul.Districts.Add(new District(istanbul.Id, "Bakirkoy", 1283999));
        istanbul.Districts.Add(new District(istanbul.Id, "Mecidiyekoy", 2222321));
        istanbul.Districts.Add(new District(istanbul.Id, "Uskudar", 726172));

        await _cityRepository.InsertAsync(new City(Guid.NewGuid(), "Tokyo"));
        await _cityRepository.InsertAsync(new City(Guid.NewGuid(), "Madrid"));
        await _cityRepository.InsertAsync(new City(LondonCityId, "London")
        {
            ExtraProperties =
                {
                    { "Population", 10_470_000 },
                    { "PhoneCode", "42" },
                    { "ZipCode", "1000" },
                    { "Guid", "a5ed8170-30b9-4580-a395-a3c2dbc031ee" }
                }
        });
        await _cityRepository.InsertAsync(istanbul);
        await _cityRepository.InsertAsync(new City(Guid.NewGuid(), "Paris"));
        await _cityRepository.InsertAsync(new City(Guid.NewGuid(), "Washington"));
        await _cityRepository.InsertAsync(new City(Guid.NewGuid(), "Sao Paulo"));
        await _cityRepository.InsertAsync(new City(Guid.NewGuid(), "Berlin"));
        await _cityRepository.InsertAsync(new City(Guid.NewGuid(), "Amsterdam"));
        await _cityRepository.InsertAsync(new City(Guid.NewGuid(), "Beijing"));
        await _cityRepository.InsertAsync(new City(Guid.NewGuid(), "Rome"));
    }

    private async Task AddPeople()
    {
        var douglas = new Person(UserDouglasId, "Douglas", 42, cityId: LondonCityId);
        douglas.Phones.Add(new Phone(douglas.Id, "123456789"));
        douglas.Phones.Add(new Phone(douglas.Id, "123456780", PhoneType.Home));

        await _personRepository.InsertAsync(douglas);

        await _personRepository.InsertAsync(new Person(UserJohnDeletedId, "John-Deleted", 33) { IsDeleted = true });

        var tenant1Person1 = new Person(Guid.NewGuid(), TenantId1 + "-Person1", 42, TenantId1);
        var tenant1Person2 = new Person(Guid.NewGuid(), TenantId1 + "-Person2", 43, TenantId1);

        await _personRepository.InsertAsync(tenant1Person1);
        await _personRepository.InsertAsync(tenant1Person2);
    }

    private async Task AddEntitiesWithPks()
    {
        await _entityWithIntPksRepository.InsertAsync(new EntityWithIntPk("Entity1"));
    }
    
    private async Task AddProducts()
    {
        await _productRepository.InsertAsync(new Product(ProductId, "Product1", decimal.One));
    }
}
