namespace Tiknas.Uow;

public interface IUnitOfWorkTransactionBehaviourProvider
{
    bool? IsTransactional { get; }
}
