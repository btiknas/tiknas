using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Table;

public class TiknasTableHeaderTagHelperService : TiknasTagHelperService<TiknasTableHeaderTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        SetTheme(context, output);
    }

    protected virtual void SetTheme(TagHelperContext context, TagHelperOutput output)
    {
        switch (TagHelper.Theme)
        {
            case TiknasTableHeaderTheme.Default:
                return;
            case TiknasTableHeaderTheme.Dark:
                output.Attributes.AddClass("thead-dark");
                return;
            case TiknasTableHeaderTheme.Light:
                output.Attributes.AddClass("thead-light");
                return;
        }
    }
}
