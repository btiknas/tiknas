using System.Collections.Generic;

namespace Tiknas.Auditing;

internal class EntityHistorySelectorList : List<NamedTypeSelector>, IEntityHistorySelectorList
{
    public bool RemoveByName(string name)
    {
        return RemoveAll(s => s.Name == name) > 0;
    }
}
