using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Shouldly;
using Tiknas.Domain.Repositories;
using Tiknas.Modularity;
using Tiknas.Specifications;
using Tiknas.TestApp.Domain;
using Xunit;

namespace Tiknas.TestApp.Testing;

public abstract class Repository_Specifications_Tests<TStartupModule> : TestAppTestBase<TStartupModule>
    where TStartupModule : ITiknasModule
{
    protected readonly IRepository<City, Guid> CityRepository;

    protected Repository_Specifications_Tests()
    {
        CityRepository = GetRequiredService<IRepository<City, Guid>>();
    }

    [Fact]
    public async Task SpecificationWithRepository_Test()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            (await CityRepository.CountAsync(new CitySpecification().ToExpression())).ShouldBe(1);
            return Task.CompletedTask;
        });
    }
}

public class CitySpecification : Specification<City>
{
    public override Expression<Func<City, bool>> ToExpression()
    {
        return city => city.Name == "Istanbul";
    }
}
