using JetBrains.Annotations;

namespace Tiknas.Uow;

public interface IUnitOfWorkAccessor
{
    IUnitOfWork? UnitOfWork { get; }

    void SetUnitOfWork(IUnitOfWork? unitOfWork);
}
