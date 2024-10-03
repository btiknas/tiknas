using JetBrains.Annotations;

namespace Tiknas.Domain.Entities.Events.Distributed;

public interface IEntityToEtoMapper
{
    object? Map(object entityObj);
}
