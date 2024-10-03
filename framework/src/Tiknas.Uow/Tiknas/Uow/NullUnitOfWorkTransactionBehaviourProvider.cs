using Tiknas.DependencyInjection;

namespace Tiknas.Uow;

public class NullUnitOfWorkTransactionBehaviourProvider : IUnitOfWorkTransactionBehaviourProvider, ISingletonDependency
{
    public bool? IsTransactional => null;
}
