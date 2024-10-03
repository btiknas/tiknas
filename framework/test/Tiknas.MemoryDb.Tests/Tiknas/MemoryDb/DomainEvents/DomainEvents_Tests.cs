using System.Threading.Tasks;
using Tiknas.TestApp.Testing;
using Xunit;

namespace Tiknas.MemoryDb.DomainEvents;

public class DomainEvents_Tests : DomainEvents_Tests<TiknasMemoryDbTestModule>
{
    [Fact(Skip = "MemoryDB doesn't support transactions.")]
    public override Task Should_Rollback_Uow_If_Event_Handler_Throws_Exception()
    {
        return base.Should_Rollback_Uow_If_Event_Handler_Throws_Exception();
    }
}
