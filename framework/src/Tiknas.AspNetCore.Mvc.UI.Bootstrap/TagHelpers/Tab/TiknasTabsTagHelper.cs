
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Grid;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Tab;

public class TiknasTabsTagHelper : TiknasTagHelper<TiknasTabsTagHelper, TiknasTabsTagHelperService>
{
    public string? Name { get; set; }

    public TabStyle TabStyle { get; set; } = TabStyle.Tab;

    public ColumnSize VerticalHeaderSize { get; set; } = ColumnSize._3;

    public TiknasTabsTagHelper(TiknasTabsTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
