using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Tiknas.Data;
using Tiknas.DependencyInjection;

namespace Tiknas.MongoDB.ConnectionStrings;

[Dependency(ReplaceServices = true)]
public class MongoDBConnectionStringChecker : IConnectionStringChecker, ITransientDependency
{
    public virtual Task<TiknasConnectionStringCheckResult> CheckAsync(string connectionString)
    {
        try
        {
            var mongoUrl = MongoUrl.Create(connectionString);
            var client = new MongoClient(mongoUrl);
            client.GetDatabase(mongoUrl.DatabaseName);
            return Task.FromResult(new TiknasConnectionStringCheckResult()
            {
                Connected = true,
                DatabaseExists = true
            });
        }
        catch (Exception e)
        {
            return Task.FromResult(new TiknasConnectionStringCheckResult());
        }
    }
}
