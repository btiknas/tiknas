using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Table;

public class TiknasTableStyleTagHelperService : TiknasTagHelperService<TiknasTableStyleTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        SetStyle(context, output);
    }

    protected virtual void SetStyle(TagHelperContext context, TagHelperOutput output)
    {
        if (TagHelper.TableStyle != TiknasTableStyle.Default)
        {
            output.Attributes.AddClass("table-" + TagHelper.TableStyle.ToString().ToLowerInvariant());
        }
    }
}
