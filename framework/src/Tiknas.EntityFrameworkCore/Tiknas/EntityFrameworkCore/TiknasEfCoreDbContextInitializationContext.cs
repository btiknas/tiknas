using Tiknas.Uow;

namespace Tiknas.EntityFrameworkCore;

public class TiknasEfCoreDbContextInitializationContext
{
    public IUnitOfWork UnitOfWork { get; }

    public TiknasEfCoreDbContextInitializationContext(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }
}
