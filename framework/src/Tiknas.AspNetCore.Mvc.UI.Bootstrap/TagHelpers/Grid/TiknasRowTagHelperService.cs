using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Grid;

public class TiknasRowTagHelperService : TiknasTagHelperService<TiknasRowTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (output.TagName == "tiknas-row")
        {
            output.Attributes.AddClass("row");
        }
        if (output.TagName == "tiknas-form-row")
        {
            output.Attributes.AddClass("row");
        }

        output.TagName = "div";

        ProcessVerticalAlign(output);
        ProcessHorizontalAlign(output);
        ProcessGutters(output);
    }

    protected virtual void ProcessVerticalAlign(TagHelperOutput output)
    {
        if (TagHelper.VAlign == VerticalAlign.Default)
        {
            return;
        }

        output.Attributes.AddClass("align-items-" + TagHelper.VAlign.ToString().ToLowerInvariant());
    }

    protected virtual void ProcessHorizontalAlign(TagHelperOutput output)
    {
        if (TagHelper.HAlign == HorizontalAlign.Default)
        {
            return;
        }

        output.Attributes.AddClass("justify-content-" + TagHelper.HAlign.ToString().ToLowerInvariant());
    }

    protected virtual void ProcessGutters(TagHelperOutput output)
    {
        if (TagHelper.Gutters ?? true)
        {
            return;
        }

        output.Attributes.AddClass("g-0");
    }
}
