using JetBrains.Annotations;

namespace Tiknas.Uow;

public interface IUnitOfWorkManager
{
    IUnitOfWork? Current { get; }

    [NotNull]
    IUnitOfWork Begin([NotNull] TiknasUnitOfWorkOptions options, bool requiresNew = false);

    [NotNull]
    IUnitOfWork Reserve([NotNull] string reservationName, bool requiresNew = false);

    void BeginReserved([NotNull] string reservationName, [NotNull] TiknasUnitOfWorkOptions options);

    bool TryBeginReserved([NotNull] string reservationName, [NotNull] TiknasUnitOfWorkOptions options);
}
