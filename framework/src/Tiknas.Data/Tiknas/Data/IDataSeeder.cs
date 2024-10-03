using System.Threading.Tasks;

namespace Tiknas.Data;

public interface IDataSeeder
{
    Task SeedAsync(DataSeedContext context);
}
