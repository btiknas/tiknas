namespace Tiknas.Uow;

public interface IAmbientUnitOfWork : IUnitOfWorkAccessor
{
    IUnitOfWork? GetCurrentByChecking();
}
