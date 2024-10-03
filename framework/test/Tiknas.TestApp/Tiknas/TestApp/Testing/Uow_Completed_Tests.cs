using System;
using System.Threading.Tasks;
using Tiknas.Modularity;
using Tiknas.TestApp.Domain;
using Tiknas.Uow;
using Xunit;

namespace Tiknas.TestApp.Testing;

public abstract class Uow_Completed_Tests<TStartupModule> : TestAppTestBase<TStartupModule>
    where TStartupModule : ITiknasModule
{
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly ICityRepository _cityRepository;

    protected Uow_Completed_Tests()
    {
        _unitOfWorkManager = GetRequiredService<IUnitOfWorkManager>();
        _cityRepository = GetRequiredService<ICityRepository>();
    }

    [Fact]
    public async Task Should_Be_Able_To_Perform_Database_Operation_On_Uow_Complete()
    {
        using (var uow = _unitOfWorkManager.Begin())
        {
            //Perform an arbitrary database operation
            await _cityRepository.InsertAsync(new City(Guid.NewGuid(), Guid.NewGuid().ToString()));

            uow.OnCompleted(async () =>
            {
                    //Perform another database operation inside the OnCompleted handler
                    await _cityRepository.InsertAsync(new City(Guid.NewGuid(), Guid.NewGuid().ToString()));
            });

            await uow.CompleteAsync();
        }
    }
}
