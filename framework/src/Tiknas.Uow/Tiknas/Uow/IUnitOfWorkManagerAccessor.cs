namespace Tiknas.Uow;

public interface IUnitOfWorkManagerAccessor
{
    IUnitOfWorkManager UnitOfWorkManager { get; }
}
