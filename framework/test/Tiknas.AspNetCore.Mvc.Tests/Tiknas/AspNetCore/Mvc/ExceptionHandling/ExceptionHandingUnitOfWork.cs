using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tiknas.Data;
using Tiknas.Uow;
using Tiknas.Users;

namespace Tiknas.AspNetCore.Mvc.ExceptionHandling;

public class ExceptionHandingUnitOfWork : UnitOfWork
{
    public ExceptionHandingUnitOfWork(
        IServiceProvider serviceProvider,
        IUnitOfWorkEventPublisher unitOfWorkEventPublisher,
        IOptions<TiknasUnitOfWorkDefaultOptions> options)
        : base(serviceProvider, unitOfWorkEventPublisher, options)
    {

    }
    public async override Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (ServiceProvider.GetRequiredService<ICurrentUser>().Id == Guid.Empty)
        {
            throw new TiknasDbConcurrencyException();
        }

        await base.SaveChangesAsync(cancellationToken);
    }
}
