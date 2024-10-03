using System.Threading.Tasks;
using Tiknas.DependencyInjection;

namespace Tiknas.Data;

public class DefaultConnectionStringChecker : IConnectionStringChecker, ITransientDependency
{
    public Task<TiknasConnectionStringCheckResult> CheckAsync(string connectionString)
    {
        return Task.FromResult(new TiknasConnectionStringCheckResult
        {
            Connected = false,
            DatabaseExists = false
        });
    }
}
