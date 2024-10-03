using System.Linq.Expressions;
using System.Threading;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Tiknas.EntityFrameworkCore.GlobalFilters;

namespace Tiknas.EntityFrameworkCore;

#pragma warning disable EF1001
public class TiknasEntityQueryProvider : EntityQueryProvider
{
    protected TiknasEfCoreCurrentDbContext TiknasEfCoreCurrentDbContext { get; }
    protected ICurrentDbContext CurrentDbContext { get; }

    public TiknasEntityQueryProvider(
        IQueryCompiler queryCompiler,
        TiknasEfCoreCurrentDbContext tiknasEfCoreCurrentDbContext,
        ICurrentDbContext currentDbContext)
        : base(queryCompiler)
    {
        TiknasEfCoreCurrentDbContext = tiknasEfCoreCurrentDbContext;
        CurrentDbContext = currentDbContext;
    }

    public override object Execute(Expression expression)
    {
        using (TiknasEfCoreCurrentDbContext.Use(CurrentDbContext.Context as ITiknasEfCoreDbFunctionContext))
        {
            return base.Execute(expression);
        }
    }

    public override TResult Execute<TResult>(Expression expression)
    {
        using (TiknasEfCoreCurrentDbContext.Use(CurrentDbContext.Context as ITiknasEfCoreDbFunctionContext))
        {
            return base.Execute<TResult>(expression);
        }
    }

    public override TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = new CancellationToken())
    {
        using (TiknasEfCoreCurrentDbContext.Use(CurrentDbContext.Context as ITiknasEfCoreDbFunctionContext))
        {
            return base.ExecuteAsync<TResult>(expression, cancellationToken);
        }
    }
}
#pragma warning restore EF1001
