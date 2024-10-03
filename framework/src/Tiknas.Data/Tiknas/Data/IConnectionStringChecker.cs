using System.Threading.Tasks;

namespace Tiknas.Data;

public interface IConnectionStringChecker
{
    Task<TiknasConnectionStringCheckResult> CheckAsync(string connectionString);
}
