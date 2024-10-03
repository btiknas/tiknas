using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Tiknas.DependencyInjection;
using Tiknas.UI;
using Tiknas.Uow;

namespace Tiknas.AspNetCore.Mvc.Uow;

[Dependency(ReplaceServices = true)]
public class TestUnitOfWork : UnitOfWork
{
    private readonly TestUnitOfWorkConfig _config;

    public TestUnitOfWork(
        IServiceProvider serviceProvider,
        IUnitOfWorkEventPublisher unitOfWorkEventPublisher,
        IOptions<TiknasUnitOfWorkDefaultOptions> options, TestUnitOfWorkConfig config)
        : base(
            serviceProvider,
            unitOfWorkEventPublisher,
            options)
    {
        _config = config;
    }

    public override Task CompleteAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        ThrowExceptionIfRequested();
        return base.CompleteAsync(cancellationToken);
    }

    private void ThrowExceptionIfRequested()
    {
        if (_config.ThrowExceptionOnComplete)
        {
            throw new UserFriendlyException(TestUnitOfWorkConfig.ExceptionOnCompleteMessage);
        }
    }
}
