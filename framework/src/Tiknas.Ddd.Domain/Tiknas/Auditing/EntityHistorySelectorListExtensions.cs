using System.Linq;
using Tiknas.Domain.Entities;

namespace Tiknas.Auditing;

public static class EntityHistorySelectorListExtensions
{
    public const string AllEntitiesSelectorName = "Tiknas.Entities.All";

    public static void AddAllEntities(this IEntityHistorySelectorList selectors)
    {
        if (selectors.Any(s => s.Name == AllEntitiesSelectorName))
        {
            return;
        }

        selectors.Add(new NamedTypeSelector(AllEntitiesSelectorName, t => typeof(IEntity).IsAssignableFrom(t)));
    }
}
