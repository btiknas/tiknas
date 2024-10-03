using System;
using System.Collections.Generic;

namespace Tiknas.Domain.Entities.Events.Distributed;

public interface IAutoEntityDistributedEventSelectorList : IList<NamedTypeSelector>
{
}
