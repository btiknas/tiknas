using System;
using System.Collections.Generic;

namespace Tiknas.Data;

public class TiknasDataFilterOptions
{
    public Dictionary<Type, DataFilterState> DefaultStates { get; }

    public TiknasDataFilterOptions()
    {
        DefaultStates = new Dictionary<Type, DataFilterState>();
    }
}
