using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Tiknas.Domain.Repositories;
using Tiknas.TestApp.Domain;
using Tiknas.TestApp.Testing;
using Tiknas.Uow;
using Xunit;

namespace Tiknas.MongoDB.Transactions;

[Collection(MongoTestCollection.Name)]
public class Transaction_Tests : TestAppTestBase<TiknasMongoDbTestModule>
{
    private readonly IBasicRepository<Person, Guid> _personRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public Transaction_Tests()
    {
        _personRepository = GetRequiredService<IBasicRepository<Person, Guid>>();
        _unitOfWorkManager = GetRequiredService<IUnitOfWorkManager>();
    }

    [Fact]
    public async Task Should_Rollback_Transaction_When_An_Exception_Is_Thrown()
    {
        var personId = Guid.NewGuid();
        const string exceptionMessage = "thrown to rollback the transaction!";

        try
        {
            await WithUnitOfWorkAsync(new TiknasUnitOfWorkOptions { IsTransactional = true }, async () =>
            {
                await _personRepository.InsertAsync(new Person(personId, "Adam", 42));
                throw new Exception(exceptionMessage);
            });
        }
        catch (Exception e) when (e.Message == exceptionMessage)
        {

        }

        var person = await _personRepository.FindAsync(personId);
        person.ShouldBeNull();
    }

    [Fact]
    public async Task Should_Rollback_Transaction_Manually()
    {
        var personId = Guid.NewGuid();

        await WithUnitOfWorkAsync(new TiknasUnitOfWorkOptions { IsTransactional = true }, async () =>
        {
            _unitOfWorkManager.Current.ShouldNotBeNull();

            await _personRepository.InsertAsync(new Person(personId, "Adam", 42));

            await _unitOfWorkManager.Current.RollbackAsync();
        });

        var person = await _personRepository.FindAsync(personId);
        person.ShouldBeNull();
    }

    [Fact]
    public async Task Should_Rollback_Transaction_Manually_With_Double_DbContext_Transaction()
    {
        var personId = Guid.NewGuid();
        var bookId = Guid.NewGuid();

        using (var scope = ServiceProvider.CreateScope())
        {
            var uowManager = scope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();

            using (uowManager.Begin(new TiknasUnitOfWorkOptions { IsTransactional = true }))
            {
                _unitOfWorkManager.Current.ShouldNotBeNull();

                await _personRepository.InsertAsync(new Person(personId, "Adam", 42));

                await _unitOfWorkManager.Current.SaveChangesAsync();

                //Will automatically rollback since not called the Complete!
            }
        }

        (await _personRepository.FindAsync(personId)).ShouldBeNull();
    }
}
