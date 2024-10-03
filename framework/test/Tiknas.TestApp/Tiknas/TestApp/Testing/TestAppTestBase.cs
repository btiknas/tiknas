using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.Modularity;
using Tiknas.TestBase;
using Tiknas.Uow;

namespace Tiknas.TestApp.Testing;

public abstract class TestAppTestBase<TStartupModule> : TiknasIntegratedTest<TStartupModule>
    where TStartupModule : ITiknasModule
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }

    #region WithUnitOfWork

    protected virtual Task WithUnitOfWorkAsync(Func<Task> func)
    {
        return WithUnitOfWorkAsync(new TiknasUnitOfWorkOptions(), func);
    }

    protected virtual async Task WithUnitOfWorkAsync(TiknasUnitOfWorkOptions options, Func<Task> action)
    {
        using (var scope = ServiceProvider.CreateScope())
        {
            var uowManager = scope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();

            using (var uow = uowManager.Begin(options))
            {
                await action();
                await uow.CompleteAsync();
            }
        }
    }

    protected virtual Task<TResult> WithUnitOfWorkAsync<TResult>(Func<Task<TResult>> func)
    {
        return WithUnitOfWorkAsync(new TiknasUnitOfWorkOptions(), func);
    }

    protected virtual async Task<TResult> WithUnitOfWorkAsync<TResult>(TiknasUnitOfWorkOptions options, Func<Task<TResult>> func)
    {
        using (var scope = ServiceProvider.CreateScope())
        {
            var uowManager = scope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();

            using (var uow = uowManager.Begin(options))
            {
                var result = await func();
                await uow.CompleteAsync();
                return result;
            }
        }
    }

    #endregion
}
