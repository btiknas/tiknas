using System.Threading.Tasks;

namespace Tiknas.Data;

public interface IDataSeedContributor
{
    Task SeedAsync(DataSeedContext context);
}
