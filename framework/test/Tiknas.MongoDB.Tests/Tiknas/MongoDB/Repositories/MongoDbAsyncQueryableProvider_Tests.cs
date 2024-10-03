using System;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Tiknas.Domain.Repositories;
using Tiknas.TestApp.Domain;
using Tiknas.TestApp.Testing;
using Tiknas.Uow;
using Xunit;

namespace Tiknas.MongoDB.Repositories;

[Collection(MongoTestCollection.Name)]
public class MongoDbAsyncQueryableProvider_Tests : TestAppTestBase<TiknasMongoDbTestModule>
{
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IRepository<Person, Guid> _personRepository;
    private readonly MongoDbAsyncQueryableProvider _mongoDbAsyncQueryableProvider;

    public MongoDbAsyncQueryableProvider_Tests()
    {
        _unitOfWorkManager = GetRequiredService<IUnitOfWorkManager>();
        _personRepository = GetRequiredService<IRepository<Person, Guid>>();
        _mongoDbAsyncQueryableProvider = GetRequiredService<MongoDbAsyncQueryableProvider>();
    }

    [Fact]
    public async Task CanExecuteAsync()
    {
        _mongoDbAsyncQueryableProvider.CanExecute(await _personRepository.GetQueryableAsync()).ShouldBeTrue();
        _mongoDbAsyncQueryableProvider.CanExecute(await _personRepository.WithDetailsAsync()).ShouldBeTrue();
    }

    [Fact]
    public async Task FirstOrDefaultAsync()
    {
        using (var uow = _unitOfWorkManager.Begin())
        {
            (await _mongoDbAsyncQueryableProvider.FirstOrDefaultAsync((await _personRepository.GetQueryableAsync()).Where(p => p.Name == "Douglas"))).ShouldNotBeNull();
            await uow.CompleteAsync();
        }
    }

    [Fact]
    public async Task AnyAsync()
    {
        using (var uow = _unitOfWorkManager.Begin())
        {
            (await _mongoDbAsyncQueryableProvider.AnyAsync(await _personRepository.GetQueryableAsync(), p => p.Name == "Douglas")).ShouldBeTrue();
            await uow.CompleteAsync();
        }
    }

    [Fact]
    public async Task CountAsync()
    {
        using (var uow = _unitOfWorkManager.Begin())
        {
            (await _mongoDbAsyncQueryableProvider.CountAsync((await _personRepository.GetQueryableAsync()).Where(p => p.Name == "Douglas"))).ShouldBeGreaterThan(0);
            await uow.CompleteAsync();
        }
    }

    [Fact]
    public async Task LongCountAsync()
    {
        using (var uow = _unitOfWorkManager.Begin())
        {
            (await _mongoDbAsyncQueryableProvider.LongCountAsync(await _personRepository.GetQueryableAsync())).ShouldBeGreaterThan(0);
            await uow.CompleteAsync();
        }
    }

    //More MongoDbAsyncQueryableProvider's method test.
}
