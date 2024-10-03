using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Tiknas.EntityFrameworkCore.ChangeTrackers;

public class TiknasEntityEntry
{
    public string Id { get; set; }

    public EntityEntry EntityEntry { get; set; }

    public List<TiknasNavigationEntry> NavigationEntries { get; set; }

    private bool _isModified;
    public bool IsModified
    {
        get
        {
            return _isModified || EntityEntry.State == EntityState.Modified || NavigationEntries.Any(n => n.IsModified);
        }
        set
        {
            _isModified = value;
        }
    }

    public TiknasEntityEntry(string id, EntityEntry entityEntry)
    {
        Id = id;
        EntityEntry = entityEntry;
        NavigationEntries = EntityEntry.Navigations.Select(x => new TiknasNavigationEntry(x, x.Metadata.Name)).ToList();
    }
}

public class TiknasNavigationEntry
{
    public NavigationEntry NavigationEntry { get; set; }

    public string Name { get; set; }

    public bool IsModified { get; set; }

    public TiknasNavigationEntry(NavigationEntry navigationEntry, string name)
    {
        NavigationEntry = navigationEntry;
        Name = name;
    }
}
