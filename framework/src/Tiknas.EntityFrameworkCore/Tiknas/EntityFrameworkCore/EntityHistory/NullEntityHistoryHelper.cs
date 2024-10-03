using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Tiknas.Auditing;
using Tiknas.EntityFrameworkCore.ChangeTrackers;

namespace Tiknas.EntityFrameworkCore.EntityHistory;

public class NullEntityHistoryHelper : IEntityHistoryHelper
{
    public static NullEntityHistoryHelper Instance { get; } = new NullEntityHistoryHelper();

    private NullEntityHistoryHelper()
    {

    }

    public void InitializeNavigationHelper(TiknasEfCoreNavigationHelper tiknasEfCoreNavigationHelper)
    {

    }

    public List<EntityChangeInfo> CreateChangeList(ICollection<EntityEntry> entityEntries)
    {
        return new List<EntityChangeInfo>();
    }

    public void UpdateChangeList(List<EntityChangeInfo> entityChanges)
    {

    }
}
