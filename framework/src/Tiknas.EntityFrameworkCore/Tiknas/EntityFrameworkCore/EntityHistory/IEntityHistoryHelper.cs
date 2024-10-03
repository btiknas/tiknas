using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Tiknas.Auditing;
using Tiknas.EntityFrameworkCore.ChangeTrackers;

namespace Tiknas.EntityFrameworkCore.EntityHistory;

public interface IEntityHistoryHelper
{
    void InitializeNavigationHelper(TiknasEfCoreNavigationHelper tiknasEfCoreNavigationHelper);

    List<EntityChangeInfo> CreateChangeList(ICollection<EntityEntry> entityEntries);

    void UpdateChangeList(List<EntityChangeInfo> entityChanges);
}
