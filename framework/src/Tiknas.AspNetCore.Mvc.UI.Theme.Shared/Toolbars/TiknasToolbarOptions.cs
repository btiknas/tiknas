using System.Collections.Generic;
using JetBrains.Annotations;

namespace Tiknas.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;

public class TiknasToolbarOptions
{
    [NotNull]
    public List<IToolbarContributor> Contributors { get; }

    public TiknasToolbarOptions()
    {
        Contributors = new List<IToolbarContributor>();
    }
}
