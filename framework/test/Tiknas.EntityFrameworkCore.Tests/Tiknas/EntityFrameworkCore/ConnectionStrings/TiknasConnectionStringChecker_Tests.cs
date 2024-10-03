using System.Threading.Tasks;
using Shouldly;
using Tiknas.Data;
using Xunit;

namespace Tiknas.EntityFrameworkCore.ConnectionStrings;

public class TiknasConnectionStringChecker_Tests : EntityFrameworkCoreTestBase
{
    [Fact]
    public async Task IsValidAsync()
    {
        var connectionStringChecker = GetRequiredService<IConnectionStringChecker>();
        var result = await connectionStringChecker.CheckAsync(@"Data Source=:memory:");
        result.Connected.ShouldBeTrue();
        result.DatabaseExists.ShouldBeTrue();
    }
}
