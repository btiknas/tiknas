using System.Collections.Generic;
using JetBrains.Annotations;

namespace Tiknas.AspNetCore.Components.Web.Theming.Toolbars;

public class TiknasToolbarOptions
{
    [NotNull]
    public List<IToolbarContributor> Contributors { get; }

    public TiknasToolbarOptions()
    {
        Contributors = new List<IToolbarContributor>();
    }
}
